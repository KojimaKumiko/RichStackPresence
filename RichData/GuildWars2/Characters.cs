using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RichData.GuildWars2
{
    public class Characters
    {
        //public Character[] CharacterArray;

        //public Characters(string APIKey)
        //{
        //    var characters = GetCharacterList(APIKey);
        //    CharacterArray = new Character[characters.Length];
        //    for(int i = 0; i < characters.Length; i++)
        //    {
        //        CharacterArray[i] = GetCharacter(characters[i], APIKey);
        //    }
        //}

        public async Task<string[]> GetCharacterListAsync(string APIKey)
        {
            using (var webClient = new WebClient() { Encoding = Encoding.UTF8 })
            {
                var json = await webClient.DownloadStringTaskAsync(Character.Address + "?access_token=" + APIKey);
                string[] characterList = JsonConvert.DeserializeObject<string[]>(json);
                return characterList;
            }
        }

        public async Task<Character> GetCharacter(string CharacterName, string APIKey)
        {
            using (var webClient = new WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(Character.Address + "/" + CharacterName + "?access_token=" + APIKey);
                Character character = JsonConvert.DeserializeObject<Character>(json);
                return character;
            }
        }

       
    }

    public struct Character
    {
        public string Name;
        public string Race;
        public string Gender;
        public string Profession;
        public int Level;
        public string Guild;
        public int Age;
        public string Created;
        public int Deaths;
        public int Title;
        public Crafting[] Crafting;
        public Equipment[] Equipment;
        public Bags?[] Bags;
        public Skills Skills;
        public Specializations Specializations;
        public Training[] Training;
        [JsonProperty(PropertyName = "wvw_abilities")]
        public WVWAbilities[] WvWAbilities;
        public Flags[] Flags;
        [JsonProperty(PropertyName = "equipment_pvp")]
        public EquipmentPVP EquipmentPVP;
        public static string Address = "https://api.guildwars2.com/v2/characters";
    }

    public struct Crafting
    {
        public string Discipline;
        public int Rating;
        public bool Active;
    }

    public struct Equipment
    {
        public int Id;
        public string Slot;
        public int[] Infusions;
        public int[] Upgrades;
        public int Skin;
        public Stats Stats;
        public string Binding;
        public int Charges;
        [JsonProperty(PropertyName = "bound_to")]
        public string BoundTo;
        public int?[] Dyes;
    }

    public struct Stats
    {
        public int Id;
        public Attributes Attributes;
    }

    public struct Attributes
    {
        public int? Power;
        public int? Precision;
        public int? Toughness;
        public int? Vitality;
        [JsonProperty(PropertyName = "Condition Damage")]
        public int? ConditionDamage;
        [JsonProperty(PropertyName = "Condition Duration")]
        public int? ConditionDuration;
        public int? Healing;
        public int? BoonDuration;
    }

    public struct Bags
    {
        public int Id;
        public int Size;
        public InventoryBag?[] Inventory;
    }

    public struct InventoryBag
    {
        public int Id;
        public int Count;
        public int?[] Infusions;
        public int?[] Uprades;
        public int? Skin;
        public Stats? stats;
        public string Binding;
        [JsonProperty(PropertyName = "bound_to")]
        public string BoundTo;
    }

    public struct Skills
    {
        public PvE? PvE;
        public PvE? PvP;
        public PvE? WvW;
    }

    public struct PvE
    {
        public int? Heal;
        public int?[] Utilities;
        public int? Elite;
        public string[] Legends;
    }

    public struct Specializations
    {
        public Specs?[] PvE;
        public Specs?[] PvP;
        public Specs?[] WvW;
    }

    public struct Specs
    {
        public int Id;
        public int?[] Traits;
    }

    public struct Training
    {
        public int Id;
        public int Spent;
        public bool Done;
    }

    public struct EquipmentPVP
    {
        public int? Amulet;
        public int? Rune;
        public int?[] sigils;
    }

    public struct Flags
    {
        public string Beta;
    }

    public struct WVWAbilities
    {
        public int Id;
        public int Rank;
    }
}
