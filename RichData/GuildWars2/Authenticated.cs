using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RichData.GuildWars2
{
    public class Authenticated
    {
        public Authenticated(string APIKey)
        {
            _apiKey = APIKey;
        }

        public Account GetAccount()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Account.Address + _apiKey);
                return JsonConvert.DeserializeObject<Account>(json);
            }
        }

        public List<Bank?> GetBank()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Bank.Address + _apiKey);
                return JsonConvert.DeserializeObject<List<Bank?>>(json);
            }
        }

        public int[] GetDyes()
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/account/dyes?access_token=" + _apiKey);
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public List<Finishers> GetFinishers()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Finishers.Address + _apiKey);
                return JsonConvert.DeserializeObject<List<Finishers>>(json);
            }
        }

        public List<Inventory> GetInventories()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Inventory.Address + _apiKey);
                return JsonConvert.DeserializeObject<List<Inventory>>(json);
            }
        }

        public List<Masteries> GetMasteries()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Masteries.Address + _apiKey);
                return JsonConvert.DeserializeObject<List<Masteries>>(json);
            }
        }

        public List<Materials> GetMaterials()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Materials.Address + _apiKey);
                return JsonConvert.DeserializeObject<List<Materials>>(json);
            }
        }

        public int[] GetMinis()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/account/minis?access_token=" + _apiKey);
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public int[] GetOutfits()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/account/outfits?access_token=" + _apiKey);
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public int[] GetRecipes()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/account/recipes?access_token=" + _apiKey);
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public int[] GetSkins()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/account/skins?access_token=" + _apiKey);
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public int[] GetTitles()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/account/titles?access_token=" + _apiKey);
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public List<Wallet> GetWallet()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Wallet.Address + _apiKey);
                return JsonConvert.DeserializeObject<List<Wallet>>(json);
            }
        }

        private string _apiKey;
    }

    public struct Account
    {
        public string Id{ get; set; }
        public string Age{ get; set; }
        public string Name{ get; set; }
        public int World{ get; set; }
        public string[] Guilds{ get; set; }
        [JsonProperty(PropertyName = "guild_leader")]
        public string[] GuildLeader{ get; set; }
        public string Created{ get; set; }
        public string[] Access{ get; set; }
        public bool Commander{ get; set; }
        [JsonProperty(PropertyName = "fractal_level")]
        public int FractalLevel{ get; set; }
        [JsonProperty(PropertyName = "daily_ap")]
        public int DailyAp{ get; set; }
        [JsonProperty(PropertyName = "monthly_ap")]
        public int MonthlyAp{ get; set; }
        [JsonProperty(PropertyName = "wvw_rank")]
        public int WvwRank{ get; set; }
        public static string Address = "https://api.guildwars2.com/v2/account?access_token=";
    }

    public struct Achievements
    {
        public int Id{ get; set; }
        public int Current{ get; set; }
        public int Max{ get; set; }
        public bool Done{ get; set; }
        public int Repeated{ get; set; }
        // Unsure as to what this is needed for
        //public int[] Bits{ get; set; }
    }

    public struct Bank
    {
        public int Id{ get; set; }
        public int Count{ get; set; }
        public int Skin{ get; set; }
        public int[] Upgrades{ get; set; }
        public int[] Infusions{ get; set; }
        public string Binding{ get; set; }
        public int Charges{ get; set; }
        [JsonProperty(PropertyName = "bound_to")]
        public string BoundTo{ get; set; }
        public static string Address = "https://api.guildwars2.com/v2/account/bank?access_token=";
    }

    public struct Finishers
    {
        public int Id{ get; set; }
        public bool Permanent{ get; set; }
        public int Quantity{ get; set; }
        public static string Address = "https://api.guildwars2.com/v2/account/finishers?access_token=";
    }

    public struct Inventory
    {
        public int Id{ get; set; }
        public int Number{ get; set; }
        public string Binding{ get; set; }
        public static string Address = "https://api.guildwars2.com/v2/account/inventory?access_token=";
    }
    
    public struct Masteries
    {
        public int Id{ get; set; }
        public int Level{ get; set; }
        public static string Address = "https://api.guildwars2.com/v2/account/masteries?access_token=";
    }

    public struct Materials
    {
        public int Id{ get; set; }
        public int Category{ get; set; }
        public int Count{ get; set; }
        public static string Address = "https://api.guildwars2.com/v2/account/materials?access_token=";
    }
    
    public struct Wallet
    {
        public int Id{ get; set; }
        public int Value{ get; set; }
        public static string Address = "https://api.guildwars2.com/v2/account/wallet?access_token=";
    }
}
