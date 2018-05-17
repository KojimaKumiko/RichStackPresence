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
        public Authenticated()
        {
            // TODO(zelTi): remove local APIKey
            string APIKey = "AF7667CF-5996-5648-9184-2C45496603CC0DAA4380-B6FD-4361-90DF-29E7C9E5739C";
            // WebClient used to download the Json from the RESTFull API
            using (var webClient = new WebClient())
            {
                //Parse the JSON to a C# Object, using the struct defined below
                var json = webClient.DownloadString(Account.Address + APIKey);
                account = JsonConvert.DeserializeObject<Account>(json);
                
                json = "";
                json = webClient.DownloadString(Bank.Address + APIKey);
                bank = JsonConvert.DeserializeObject<List<Bank?>>(json);
                // Remove all null values in the List (null = empty slot)
                bank.RemoveAll(b => b.HasValue == false);

                json = "";
                json = webClient.DownloadString(DyesAddress + APIKey);
                dyes = JsonConvert.DeserializeObject<int[]>(json);

                json = "";
                json = webClient.DownloadString(Finishers.Address + APIKey);
                finishers = JsonConvert.DeserializeObject<List<Finishers>>(json);

                json = "";
                json = webClient.DownloadString(Inventory.Address + APIKey);
                inventory = JsonConvert.DeserializeObject<List<Inventory>>(json);

                json = "";
                json = webClient.DownloadString(Masteries.Address + APIKey);
                masteries = JsonConvert.DeserializeObject<List<Masteries>>(json);

                json = "";
                json = webClient.DownloadString(Materials.Address + APIKey);
                materials = JsonConvert.DeserializeObject<List<Materials>>(json);

                json = "";
                json = webClient.DownloadString(MiniAddress + APIKey);
                minis = JsonConvert.DeserializeObject<int[]>(json);

                json = "";
                json = webClient.DownloadString(OutfitsAddress + APIKey);
                outfits = JsonConvert.DeserializeObject<int[]>(json);

                json = "";
                json = webClient.DownloadString(RecipeAddress + APIKey);
                recipes = JsonConvert.DeserializeObject<int[]>(json);

                json = "";
                json = webClient.DownloadString(SkinAddress + APIKey);
                skins = JsonConvert.DeserializeObject<int[]>(json);

                json = "";
                json = webClient.DownloadString(TitleAddress + APIKey);
                titles = JsonConvert.DeserializeObject<int[]>(json);

                json = "";
                json = webClient.DownloadString(Wallet.Address + APIKey);
                wallet = JsonConvert.DeserializeObject<List<Wallet>>(json);
            }
        }

        private Account account;
        private List<Bank?> bank;
        private string DyesAddress = "https://api.guildwars2.com/v2/account/dyes?access_token=";
        private int[] dyes;
        private List<Finishers> finishers;
        private List<Inventory> inventory;
        private List<Masteries> masteries;
        private List<Materials> materials;
        private string MiniAddress = "https://api.guildwars2.com/v2/account/minis?access_token=";
        private int[] minis;
        private string OutfitsAddress = "https://api.guildwars2.com/v2/account/outfits?access_token=";
        private int[] outfits;
        private string RecipeAddress = "https://api.guildwars2.com/v2/account/recipes?access_token=";
        private int[] recipes;
        private string SkinAddress = "https://api.guildwars2.com/v2/account/skins?access_token=";
        private int[] skins;
        private string TitleAddress = "https://api.guildwars2.com/v2/account/titles?access_token=";
        private int[] titles;
        private List<Wallet> wallet;

    }

    public struct Account
    {
        public string Id;
        public string Age;
        public string Name;
        public int World;
        public string[] Guilds;
        [JsonProperty(PropertyName = "guild_leader")]
        public string[] GuildLeader;
        public string Created;
        public string[] Access;
        public bool Commander;
        [JsonProperty(PropertyName = "fractal_level")]
        public int FractalLevel;
        [JsonProperty(PropertyName = "daily_ap")]
        public int DailyAp;
        [JsonProperty(PropertyName = "monthly_ap")]
        public int MonthlyAp;
        [JsonProperty(PropertyName = "wvw_rank")]
        public int WvwRank;
        public static string Address = "https://api.guildwars2.com/v2/account?access_token=";
    }

    public struct Achievements
    {
        public int Id;
        public int Current;
        public int Max;
        public bool Done;
        public int Repeated;
        // Unsure as to what this is needed for
        //public int[] Bits;
    }

    public struct Bank
    {
        public int Id;
        public int Count;
        public int Skin;
        public int[] Upgrades;
        public int[] Infusions;
        public string Binding;
        public int Charges;
        [JsonProperty(PropertyName = "bound_to")]
        public string BoundTo;
        public static string Address = "https://api.guildwars2.com/v2/account/bank?access_token=";
    }

    public struct Finishers
    {
        public int Id;
        public bool Permanent;
        public int Quantity;
        public static string Address = "https://api.guildwars2.com/v2/account/finishers?access_token=";
    }

    public struct Inventory
    {
        public int Id;
        public int Number;
        public string Binding;
        public static string Address = "https://api.guildwars2.com/v2/account/inventory?access_token=";
    }
    
    public struct Masteries
    {
        public int Id;
        public int Level;
        public static string Address = "https://api.guildwars2.com/v2/account/masteries?access_token=";
    }

    public struct Materials
    {
        public int Id;
        public int Category;
        public int Count;
        public static string Address = "https://api.guildwars2.com/v2/account/materials?access_token=";
    }
    
    public struct Wallet
    {
        public int Id;
        public int Value;
        public static string Address = "https://api.guildwars2.com/v2/account/wallet?access_token=";
    }
}
