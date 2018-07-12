using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using MingweiSamuel.Camille.LolStaticData;
using MingweiSamuel.Camille.Match;
using RichData;

namespace RichWebsiteV2.Controllers
{
    public class AccountLoLController : Controller
    {
        // GET: AccountLoL
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LoL(string regi, string SummonerName)
        {
            try
            {
                if (Session["RitoKey"] != null && Session["RitoAPI"] != null)
                {
                    var riotApi = (RiotApi)Session["RitoAPI"];
                    var region = Region.PBE;
                    switch (regi)
                    {
                        case "PBE":
                            region = Region.PBE;
                            break;
                        case "TR":
                            region = Region.TR;
                            break;
                        case "RU":
                            region = Region.RU;
                            break;
                        case "OCE":
                            region = Region.OCE;
                            break;
                        case "LAS":
                            region = Region.LAS;
                            break;
                        case "LAN":
                            region = Region.LAN;
                            break;
                        case "KR":
                            region = Region.KR;
                            break;
                        case "NA":
                            region = Region.NA;
                            break;
                        case "EUW":
                            region = Region.EUW;
                            break;
                        case "EUNE":
                            region = Region.EUNE;
                            break;
                        case "JP":
                            region = Region.JP;
                            break;
                        case "BR":
                            region = Region.BR;
                            break;
                        default:
                            break;
                    }

                    int matchNr = 0;
                    Match Match0, Match1, Match2, Match3, Match4, Match5, Match6, Match7, Match8, Match9;
                    var Champs = (IDictionary<string, Champion>)Session["Champs"];
                    List<string> idList = new List<string>();
                    var summoner = await riotApi.Summoner.GetBySummonerNameAsync(region, SummonerName);
                    var filePath = "http://ddragon.leagueoflegends.com/cdn/" + Session["Version"].ToString() + "/img/profileicon/" + summoner.ProfileIconId.ToString() + ".png";
                    var sumID = summoner.Id;
                    var accID = summoner.AccountId;
                    var matchList = await riotApi.Match.GetMatchlistAsync(region, accID, endIndex: 10);

                    foreach (var match in matchList.Matches)
                    {
                        var listedMatch = await riotApi.Match.GetMatchAsync(region, match.GameId);
                        switch (matchNr)
                        {
                            case 0:
                                Match0 = listedMatch;
                                ViewBag.GameMode = Match0.GameMode;
                                long sec = Match0.GameDuration;
                                int min = (int)sec / 60;
                                sec = sec - min * 60;
                                ViewBag.Duration = min + "m : " + sec + "s";
                                ViewBag.Sum0 = Match0.ParticipantIdentities[0].Player.SummonerName;
                                ViewBag.Champ0 = Champs[Match0.Participants[0].ChampionId.ToString()].Name;
                                ViewBag.Sum1 = Match0.ParticipantIdentities[1].Player.SummonerName;
                                ViewBag.Champ1 = Champs[Match0.Participants[1].ChampionId.ToString()].Name;
                                ViewBag.Sum2 = Match0.ParticipantIdentities[2].Player.SummonerName;
                                ViewBag.Champ2 = Champs[Match0.Participants[2].ChampionId.ToString()].Name;
                                ViewBag.Sum3 = Match0.ParticipantIdentities[3].Player.SummonerName;
                                ViewBag.Champ3 = Champs[Match0.Participants[3].ChampionId.ToString()].Name;
                                ViewBag.Sum4 = Match0.ParticipantIdentities[4].Player.SummonerName;
                                ViewBag.Champ4 = Champs[Match0.Participants[4].ChampionId.ToString()].Name;
                                ViewBag.Sum5 = Match0.ParticipantIdentities[5].Player.SummonerName;
                                ViewBag.Champ5 = Champs[Match0.Participants[5].ChampionId.ToString()].Name;
                                ViewBag.Sum6 = Match0.ParticipantIdentities[6].Player.SummonerName;
                                ViewBag.Champ6 = Champs[Match0.Participants[6].ChampionId.ToString()].Name;
                                ViewBag.Sum7 = Match0.ParticipantIdentities[7].Player.SummonerName;
                                ViewBag.Champ7 = Champs[Match0.Participants[7].ChampionId.ToString()].Name;
                                ViewBag.Sum8 = Match0.ParticipantIdentities[8].Player.SummonerName;
                                ViewBag.Champ8 = Champs[Match0.Participants[8].ChampionId.ToString()].Name;
                                ViewBag.Sum9 = Match0.ParticipantIdentities[9].Player.SummonerName;
                                ViewBag.Champ9 = Champs[Match0.Participants[9].ChampionId.ToString()].Name;
                                if (Match0.Teams[0].Win == "Win")
                                {
                                    ViewBag.Winner = "Red team won";
                                }
                                else
                                {
                                    ViewBag.Winner = "Blue team won";
                                }



                                break;
                            case 1:
                                Match1 = listedMatch;
                                break;
                            case 2:
                                Match2 = listedMatch;
                                break;
                            case 3:
                                Match3 = listedMatch;
                                break;
                            case 4:
                                Match4 = listedMatch;
                                break;
                            case 5:
                                Match5 = listedMatch;
                                break;
                            case 6:
                                Match6 = listedMatch;
                                break;
                            case 7:
                                Match7 = listedMatch;
                                break;
                            case 8:
                                Match8 = listedMatch;
                                break;
                            case 9:
                                Match9 = listedMatch;
                                break;
                            default:
                                break;
                        }

                        matchNr++;
                    }


                    ViewBag.SummonerName = summoner.Name;
                    ViewBag.SummonerIcon = filePath;
                    ViewBag.lvl = "LVL: " + summoner.SummonerLevel;
                }
                else
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Config/LeagueAPIKey.json")))
                    {
                        Session["RitoKey"] = Config.LoadFromFile(Server.MapPath("~/Config/LeagueAPIKey.json")).Key;
                        var riotApi = RiotApi.NewInstance(Session["RitoKey"].ToString());
                        Session["RitoAPI"] = riotApi;
                        string[] versions = await riotApi.LolStaticData.GetVersionsAsync(Region.EUW);
                        Session["version"] = versions[0];/*"8.13.1"; //*/
                        var Champs = (await riotApi.LolStaticData.GetChampionListAsync(Region.EUW, dataById: true)).Data;
                        Session["Champs"] = Champs;
                    }
                    else
                    {
                        new Config().SaveToFile(Server.MapPath("~/Config/LeagueAPIKey.json"), Server.MapPath("~/Config"));
                    }
                    ViewBag.SummonerIcon = "http://ddragon.leagueoflegends.com/cdn/8.13.1/img/profileicon/3179.png";
                }
            }
            catch (Exception)
            {
                
            }
           
            return View();
        }

        public ActionResult SummonerInfos()
        {
            return View();
        }
    }
}