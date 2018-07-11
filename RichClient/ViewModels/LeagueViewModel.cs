using Caliburn.Micro;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.ChampionMastery;
using MingweiSamuel.Camille.Enums;
using MingweiSamuel.Camille.LolStaticData;
using MingweiSamuel.Camille.Match;
using RichData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RichClient.ViewModels
{
    public sealed class LeagueViewModel : Screen, IMainViewTabItem
    {
        public LeagueViewModel()
        {
            DisplayName = "League of Legends";
            LolRegions = new BindableCollection<string>();
            LolRegions2 = new BindableCollection<string>();
            LolRegions3 = new BindableCollection<string>();
            ChampionName = new BindableCollection<string>();
            ChampionIcon = new BindableCollection<string>();
            ChampionPoints = new BindableCollection<int>();
            ChampionLevel = new BindableCollection<int>();
            MatchHistoryDictionary = new ObservableDictionary<MatchReference, string>();
            SummonerListBlueSide = new BindableCollection<string>();
            SummonerIconListBlueSide = new BindableCollection<string>();
            ChampionsBlueSide = new BindableCollection<string>();
            SummonerListRedSide = new BindableCollection<string>();
            SummonerIconListRedSide = new BindableCollection<string>();
            ChampionsRedSide = new BindableCollection<string>();
            BannedChampionsBlueSide = new BindableCollection<string[]>();
            BannedChampionsIconBlueSide = new BindableCollection<string[]>();
            BannedChampionsIconRedSide = new BindableCollection<string[]>();
            BannedChampionsRedSide = new BindableCollection<string[]>();

            LolRegion = "EUW";

            ShowError = false;
            ShowLeague = true;
            ShowSummoner = false;
            ShowMasteries = false;
            ShowMatches = false;
            CheckKey();
        }

        public async void CheckKey()
        {
            if (LeagueKey.GetKey() != null)
            {
                try
                {
                    _riotApi = RiotApi.NewInstance(LeagueKey.GetKey());
                    FillRegions();
                    champs = _riotApi.LolStaticData.GetChampionListAsync(Region.EUW, dataById: true, tags: tags);
                    spells = _riotApi.LolStaticData.GetSummonerSpellListAsync(Region.EUW, dataById: true);

                    Console.WriteLine("\nAsync Started\n");
                    SummonerSpells = (await spells).Data;
                    Versions = await _riotApi.LolStaticData.GetVersionsAsync(Region.EUW);
                    Champions = (await champs).Data;
                    Console.WriteLine("\nAsync Finished\n");
                }
                catch (Exception ex)
                {
                    ShowLeague = false;
                    ShowError = true;
                    Console.WriteLine("An exception occured: " + ex + "\nException Message: " + ex.Message);
                }
            }
            else
            {
                ShowLeague = false;
                ShowError = true;
            }
        }

        private void FillRegions()
        {
            LolRegions.Add("EUW");
            LolRegions.Add("EUNE");
            LolRegions.Add("NA");
            LolRegions.Add("TR");
            LolRegions.Add("RU");
            LolRegions.Add("OCE");
            LolRegions.Add("KR");
            LolRegions.Add("LAS");
            LolRegions.Add("LAN");
            LolRegions.Add("JP");

            LolRegions2 = LolRegions;
            LolRegions3 = LolRegions;
        }

        public void RegionChanged(string region)
        {
            LolRegion = region;
        }

        public async void ShowSummonerInformation()
        {
            if (!string.IsNullOrWhiteSpace(SummonerName))
            {
                var summoner = await _riotApi.Summoner.GetBySummonerNameAsync(GetRegion(LolRegion), SummonerName);
                if(summoner != null)
                {
                    SummonerLevel = (int)summoner.SummonerLevel;
                    if(Versions != null)
                    {
                        SummonerIcon = $"http://ddragon.leagueoflegends.com/cdn/{Versions[0]}/img/profileicon/{summoner.ProfileIconId}.png";
                    }
                    var leagues = await _riotApi.League.GetAllLeaguePositionsForSummonerAsync(GetRegion(LolRegion), summoner.Id);
                    if(leagues != null)
                    {
                        foreach (var league in leagues)
                        {
                            if (league.QueueType == "RANKED_SOLO_5x5")
                            {
                                RankedQue = "Ranked Solo/Duo";
                                RankedName = league.LeagueName;
                                RankedRank = league.Tier + " " + league.Rank;
                            }
                            else if (league.QueueType == "RANKED_TEAM_5x5")
                            {
                                FlexRanked = "Ranked Flex";
                                FlexRankedName = league.LeagueName;
                                FlexRankedRank = league.Tier + " " + league.Rank;
                            }
                        }
                    }
                    else
                    {
                        RankedName = "";
                        RankedQue = "";
                        RankedRank = "";
                        FlexRanked = "";
                        FlexRankedName = "";
                        FlexRankedRank = "";
                    }
                    ShowSummoner = true;
                }
            }
        }

        public async void SearchMastery()
        {
            if (!string.IsNullOrWhiteSpace(SummonerName))
            {
                var summoner = await _riotApi.Summoner.GetBySummonerNameAsync(GetRegion(LolRegion), SummonerName);
                if(summoner != null)
                {
                    var masteries = await _riotApi.ChampionMastery.GetAllChampionMasteriesAsync(GetRegion(LolRegion), summoner.Id);
                    for(int index = 0; index < 10; index++)
                    {
                        if(Champions != null && Versions != null)
                        {
                            ChampionName.Add(Champions[masteries[index].ChampionId.ToString()].Name);
                            var image = Champions[masteries[index].ChampionId.ToString()].Image.Full;
                            var imagePath = $"https://ddragon.leagueoflegends.com/cdn/{Versions[0]}/img/champion/{image}";
                            ChampionIcon.Add(imagePath);
                        }
                        ChampionPoints.Add(masteries[index].ChampionPoints);
                        ChampionLevel.Add(masteries[index].ChampionLevel);
                    }
                }
                ShowMasteries = true;
            }
        }

        public async void ShowRecentMatches()
        {
            var summoner = await _riotApi.Summoner.GetBySummonerNameAsync(GetRegion(LolRegion), SummonerName);
            if(summoner != null)
            {
                var matchHistory = await _riotApi.Match.GetMatchlistAsync(GetRegion(LolRegion), summoner.AccountId, endIndex: 10);
                foreach(var match in matchHistory.Matches)
                {
                    if(Champions != null)
                    {
                        MatchHistoryDictionary.Add(match, Champions[match.Champion.ToString()].Name);
                    }
                }
                ShowMatches = true;
            }
        }

        public async void ShowMatchDetails(KeyValuePair<MatchReference, string> MatchReferencePair)
        {
            var matchKey = MatchReferencePair.Key;
            var game = await _riotApi.Match.GetMatchAsync(GetRegion(LolRegion), matchKey.GameId);
            foreach(var participant in game.Participants)
            {
                var summoner = game.ParticipantIdentities.Where(pi => pi.ParticipantId == participant.ParticipantId).FirstOrDefault();
                if (participant.TeamId == 100)
                {
                    if(summoner != null)
                    {
                        SummonerListBlueSide.Add(summoner.Player.SummonerName + " ");
                        SummonerIconListBlueSide.Add($"http://ddragon.leagueoflegends.com/cdn/{Versions[0]}/img/profileicon/{summoner.Player.ProfileIcon}.png");
                        ChampionsBlueSide.Add(Champions[participant.ChampionId.ToString()].Name);
                    }
                }
                else
                {
                    if(summoner != null)
                    {
                        SummonerListRedSide.Add(summoner.Player.SummonerName + " ");
                        SummonerIconListRedSide.Add($"http://ddragon.leagueoflegends.com/cdn/{Versions[0]}/img/profileicon/{summoner.Player.ProfileIcon}.png");
                        ChampionsRedSide.Add(Champions[participant.ChampionId.ToString()].Name);
                    }
                }
            }
            foreach(var team in game.Teams)
            {
                if(team.TeamId == 100)
                {
                    List<string> bans = new List<string>();
                    List<string> icons = new List<string>();
                    foreach(var ban in team.Bans)
                    {
                        bans.Add(Champions[ban.ChampionId.ToString()].Name);
                        var path = Champions[ban.ChampionId.ToString()].Image.Full;
                        icons.Add($"https://ddragon.leagueoflegends.com/cdn/{Versions[0]}/img/champion/{path}");
                    }
                    BannedChampionsBlueSide.Add(bans.ToArray());
                    BannedChampionsIconBlueSide.Add(icons.ToArray());
                }
                else
                {
                    List<string> bans = new List<string>();
                    List<string> icons = new List<string>();
                    foreach (var ban in team.Bans)
                    {
                        bans.Add(Champions[ban.ChampionId.ToString()].Name);
                        var path = Champions[ban.ChampionId.ToString()].Image.Full;
                        icons.Add($"https://ddragon.leagueoflegends.com/cdn/{Versions[0]}/img/champion/{path}");
                    }
                    BannedChampionsRedSide.Add(bans.ToArray());
                    BannedChampionsIconRedSide.Add(icons.ToArray());
                }
            }
        }

        public void SummonerInfoKeyDown(KeyEventArgs keyEvent)
        {
            if (keyEvent.Key == Key.Enter)
            {
                ShowSummonerInformation();
            }
        }

        public void ChampionMasteriesKeyDown(KeyEventArgs keyEvent)
        {
            if (keyEvent.Key == Key.Enter)
            {
                SearchMastery();
            }
        }

        public void MatchHistoryKeyDown(KeyEventArgs keyEvent)
        {
            if (keyEvent.Key == Key.Enter)
            {
                ShowRecentMatches();
            }
        }

        #region Properties
        public Region GetRegion(string region)
        {
            Region lolRegion;
            switch (region.ToLower())
            {
                case "euw":
                    lolRegion = Region.EUW;
                    break;
                case "NA":
                    lolRegion = Region.NA;
                    break;
                case "tr":
                    lolRegion = Region.TR;
                    break;
                case "ru":
                    lolRegion = Region.RU;
                    break;
                case "oce":
                    lolRegion = Region.OCE;
                    break;
                case "kr":
                    lolRegion = Region.KR;
                    break;
                case "las":
                    lolRegion = Region.LAS;
                    break;
                case "lan":
                    lolRegion = Region.LAN;
                    break;
                case "eune":
                    lolRegion = Region.EUNE;
                    break;
                case "jp":
                    lolRegion = Region.JP;
                    break;
                default:
                    lolRegion = Region.EUW;
                    break;
            }
            return lolRegion;
        }

        private bool _showError;
        public bool ShowError
        {
            get { return _showError; }
            set
            {
                if(value == _showError) { return; }
                _showError = value;
                NotifyOfPropertyChange(() => ShowError);
            }
        }

        private bool _showLeague;
        public bool ShowLeague
        {
            get { return _showLeague; }
            set
            {
                if(value == _showLeague) { return; }
                _showLeague = value;
                NotifyOfPropertyChange(() => ShowLeague);
            }
        }

        private string _summonerName;
        public string SummonerName
        {
            get { return _summonerName; }
            set
            {
                if (value == _summonerName)
                    return;
                _summonerName = value;
                NotifyOfPropertyChange(() => SummonerName);
            }
        }

        private string _rankedQue;
        public string RankedQue
        {
            get { return _rankedQue; }
            set
            {
                if (value == _rankedQue)
                    return;
                _rankedQue = value;
                NotifyOfPropertyChange(() => RankedQue);
            }
        }

        private string _rankedRank;
        public string RankedRank
        {
            get { return _rankedRank; }
            set
            {
                if (value == _rankedRank)
                    return;
                _rankedRank = value;
                NotifyOfPropertyChange(() => RankedRank);
            }
        }

        private string _rankedName;
        public string RankedName
        {
            get { return _rankedName; }
            set
            {
                if (value == _rankedName)
                    return;
                _rankedName = value;
                NotifyOfPropertyChange(() => RankedName);
            }
        }

        private string _summonerIcon;
        public string SummonerIcon
        {
            get { return _summonerIcon; }
            set
            {
                if (value == _summonerIcon)
                    return;
                _summonerIcon = value;
                NotifyOfPropertyChange(() => SummonerIcon);
            }
        }

        private int _summonerLevel;
        public int SummonerLevel
        {
            get { return _summonerLevel; }
            set
            {
                if (value == _summonerLevel)
                    return;
                _summonerLevel = value;
                NotifyOfPropertyChange(() => SummonerLevel);
            }
        }

        private bool _showMasteries;
        public bool ShowMasteries
        {
            get { return _showMasteries; }
            set
            {
                if (value == _showMasteries)
                    return;
                _showMasteries = value;
                NotifyOfPropertyChange(() => ShowMasteries);
            }
        }

        private bool _showSummoner;
        public bool ShowSummoner
        {
            get { return _showSummoner; }
            set
            {
                if (value == _showSummoner)
                    return;
                _showSummoner = value;
                NotifyOfPropertyChange(() => ShowSummoner);
            }
        }

        private bool _showMatches;
        public bool ShowMatches
        {
            get { return _showMatches; }
            set
            {
                if (value == _showMatches)
                    return;
                _showMatches = value;
                NotifyOfPropertyChange(() => ShowMatches);
            }
        }

        private string _flexRankedName;
        public string FlexRankedName
        {
            get { return _flexRankedName; }
            set
            {
                if (value == _flexRankedName)
                    return;
                _flexRankedName = value;
                NotifyOfPropertyChange(() => FlexRankedName);
            }
        }

        private string _flexRanked;
        public string FlexRanked
        {
            get { return _flexRanked; }
            set
            {
                if (value == _flexRanked)
                    return;
                _flexRanked = value;
                NotifyOfPropertyChange(() => FlexRanked);
            }
        }

        private string _flexRankedRank;
        public string FlexRankedRank
        {
            get { return _flexRankedRank; }
            set
            {
                if (value == _flexRankedRank)
                    return;
                _flexRankedRank = value;
                NotifyOfPropertyChange(() => FlexRankedRank);
            }
        }

        public string LolRegion { get; set; }
        public string[] Versions { get; set; }
        public BindableCollection<string> LolRegions { get; set; }
        public BindableCollection<string> LolRegions2 { get; set; }
        public BindableCollection<string> LolRegions3{ get; set; }
        public BindableCollection<string> ChampionName { get; set; }
        public BindableCollection<string> ChampionIcon { get; set; }
        public BindableCollection<int> ChampionPoints { get; set; }
        public BindableCollection<int> ChampionLevel { get; set; }
        
        public BindableCollection<string> SummonerListBlueSide { get; set; }
        public BindableCollection<string> SummonerIconListBlueSide { get; set; }
        public BindableCollection<string> ChampionsBlueSide { get; set; }
        public BindableCollection<string[]> BannedChampionsBlueSide { get; set; }
        public BindableCollection<string[]> BannedChampionsIconBlueSide { get; set; }
        public BindableCollection<string> SummonerListRedSide { get; set; }
        public BindableCollection<string> SummonerIconListRedSide { get; set; }
        public BindableCollection<string> ChampionsRedSide { get; set; }
        public BindableCollection<string[]> BannedChampionsRedSide { get; set; }
        public BindableCollection<string[]> BannedChampionsIconRedSide { get; set; }
        public ObservableDictionary<MatchReference, string> MatchHistoryDictionary { get; set; }

        private static RiotApi _riotApi;
        private static string[] tags = { "skins", "image" };
        private Task<ChampionList> champs;
        public static IDictionary<string, Champion> Champions { get; set; }
        private Task<SummonerSpellList> spells;
        public static IDictionary<string, SummonerSpell> SummonerSpells { get; set; }
        #endregion
    }
}
