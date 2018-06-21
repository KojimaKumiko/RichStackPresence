using Caliburn.Micro;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using MingweiSamuel.Camille.LolStaticData;
using RichData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public sealed class LeagueViewModel : Screen, IMainViewTabItem
    {
        public LeagueViewModel()
        {
            DisplayName = "League of Legends";
            ChampionMastery = new BindableCollection<string>();
            ShowError = false;
            ShowLeague = true;
            CheckKey();
        }

        public async void CheckKey()
        {
            if (LeagueKey.GetKey() != null)
            {
                _riotApi = RiotApi.NewInstance(LeagueKey.GetKey());
                champs = _riotApi.LolStaticData.GetChampionListAsync(Region.EUW, dataById: true, tags: tags);
                spells = _riotApi.LolStaticData.GetSummonerSpellListAsync(Region.EUW, dataById: true);

                Console.WriteLine("\nAsync Started\n");
                //SummonerSpells = (await spells).Data;
                Console.WriteLine("\nSpells finished\n");
                //Champions = (await champs).Data;
                Console.WriteLine("\nAsync Finished\n");
            }
            else
            {
                ShowError = true;
            }
        }

        public void SearchMastery()
        {
            //    var summoner = _riotApi.Summoner.GetBySummonerName(Region.EUW, SummonerName);
            //    Console.WriteLine(summoner.Name);
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

        public BindableCollection<string> ChampionMastery { get; set; }
        public string SummonerName { get; set; }

        private static RiotApi _riotApi;
        private static string[] tags = { "skins" };
        private Task<ChampionList> champs;
        public static IDictionary<string, Champion> Champions { get; set; }
        private Task<SummonerSpellList> spells;
        public static IDictionary<string, SummonerSpell> SummonerSpells { get; set; }
    }
}
