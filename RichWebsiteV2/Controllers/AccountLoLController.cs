using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
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
                            Session["Match1"]=listedMatch;
                            break;
                        case 1:
                            Session["Match2"] = listedMatch;
                            break;
                        case 2:
                            Session["Match3"] = listedMatch;
                            break;
                        case 3:
                            Session["Match4"] = listedMatch;
                            break;
                        case 4:
                            Session["Match5"] = listedMatch;
                            break;
                        case 5:
                            Session["Match6"] = listedMatch;
                            break;
                        case 6:
                            Session["Match7"] = listedMatch;
                            break;
                        case 7:
                            Session["Match8"] = listedMatch;
                            break;
                        case 8:
                            Session["Match9"] = listedMatch;
                            break;
                        case 9:
                            Session["Match10"] = listedMatch;
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
                    //string[] versions = await riotApi.LolStaticData.GetVersionsAsync(Region.EUW);
                    Session["version"] = "8.13.1"; //versions[0];
                }
                else
                {
                    new Config().SaveToFile(Server.MapPath("~/Config/LeagueAPIKey.json"), Server.MapPath("~/Config"));
                }
            ViewBag.SummonerIcon = "http://ddragon.leagueoflegends.com/cdn/8.13.1/img/profileicon/3179.png";
            }
            return View();
        }

        public ActionResult SummonerInfos()
        {
            return View();
        }
    }
}