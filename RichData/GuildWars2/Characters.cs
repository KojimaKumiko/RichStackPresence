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

        public async Task<Character> GetCharacterAsync(string CharacterName, string APIKey)
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
        public string Name { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public int Level { get; set; }
        public string Guild { get; set; }
        public int Age { get; set; }
        public string Created { get; set; }
        public int Deaths { get; set; }
        public int Title { get; set; }
        public Crafting[] Crafting { get; set; }
        public Equipment[] Equipment { get; set; }
        public Bags?[] Bags { get; set; }
        public Skills Skills { get; set; }
        public Specializations Specializations { get; set; }
        public Training[] Training { get; set; }
        [JsonProperty(PropertyName = "wvw_abilities")]
        public WVWAbilities[] WvWAbilities { get; set; }
        public Flags[] Flags { get; set; }
        [JsonProperty(PropertyName = "equipment_pvp")]
        public EquipmentPVP EquipmentPVP { get; set; }
        public static string Address = "https://api.guildwars2.com/v2/characters";
    }

    public struct Crafting
    {
        public string Discipline { get; set; }
        public int Rating { get; set; }
        public bool Active { get; set; }
    }

    public struct Equipment
    {
        public int Id{ get; set; }
        public string Slot{ get; set; }
        public int[] Infusions{ get; set; }
        public int[] Upgrades{ get; set; }
        public int Skin{ get; set; }
        public Stats Stats{ get; set; }
        public string Binding{ get; set; }
        public int Charges{ get; set; }
        [JsonProperty(PropertyName = "bound_to")]
        public string BoundTo{ get; set; }
        public int?[] Dyes{ get; set; }
    }

    public struct Stats
    {
        public int Id{ get; set; }
        public Attributes Attributes{ get; set; }
    }

    public struct Attributes
    {
        public int? Power{ get; set; }
        public int? Precision{ get; set; }
        public int? Toughness{ get; set; }
        public int? Vitality{ get; set; }
        [JsonProperty(PropertyName = "Condition Damage")]
        public int? ConditionDamage{ get; set; }
        [JsonProperty(PropertyName = "Condition Duration")]
        public int? ConditionDuration{ get; set; }
        public int? Healing{ get; set; }
        public int? BoonDuration{ get; set; }
    }

    public struct Bags
    {
        public int Id{ get; set; }
        public int Size{ get; set; }
        public InventoryBag?[] Inventory{ get; set; }
    }

    public struct InventoryBag
    {
        public int Id{ get; set; }
        public int Count{ get; set; }
        public int?[] Infusions{ get; set; }
        public int?[] Uprades{ get; set; }
        public int? Skin{ get; set; }
        public Stats? stats{ get; set; }
        public string Binding{ get; set; }
        [JsonProperty(PropertyName = "bound_to")]
        public string BoundTo{ get; set; }
    }

    public struct Skills
    {
        public PvE? PvE{ get; set; }
        public PvE? PvP{ get; set; }
        public PvE? WvW{ get; set; }
    }

    public struct PvE
    {
        public int? Heal{ get; set; }
        public int?[] Utilities{ get; set; }
        public int? Elite{ get; set; }
        public string[] Legends{ get; set; }
    }

    public struct Specializations
    {
        public Specs?[] PvE{ get; set; }
        public Specs?[] PvP{ get; set; }
        public Specs?[] WvW{ get; set; }
    }

    public struct Specs
    {
        public int Id{ get; set; }
        public int?[] Traits{ get; set; }
    }

    public struct Training
    {
        public int Id{ get; set; }
        public int Spent{ get; set; }
        public bool Done{ get; set; }
    }

    public struct EquipmentPVP
    {
        public int? Amulet{ get; set; }
        public int? Rune{ get; set; }
        public int?[] sigils{ get; set; }
    }

    public struct Flags
    {
        public string Beta{ get; set; }
    }

    public struct WVWAbilities
    {
        public int Id{ get; set; }
        public int Rank{ get; set; }
    }
}
