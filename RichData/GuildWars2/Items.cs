using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RichData.GuildWars2
{
    public class Items
    {
        public int[] ItemList()
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Adress);
                var itemList = JArray.Parse(json).ToObject<int[]>();
                return itemList;
            }
        }

        public Item GetItem(int ID)
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Adress + ID.ToString());
                var item = JsonConvert.DeserializeObject<Item>(json);

                var details = JObject.Parse(json);
                var results = details.SelectToken("details");

                switch (item.Type)
                {
                    case "Armor":
                        item.Details = results.ToObject<Armor>();
                        break;
                    case "Back":
                        item.Details = results.ToObject<Back>();
                        break;
                    case "Bag":
                        item.Details = results.ToObject<Bag>();
                        break;
                    case "Consumable":
                        item.Details = results.ToObject<Consumable>();
                        break;
                    case "Container":
                        item.Details = results.ToObject<Container>();
                        break;
                    case "Gathering":
                        item.Details = results.ToObject<Gathering>();
                        break;
                    case "Gizmo":
                        item.Details = results.ToObject<Gizmo>();
                        break;
                    case "MiniPet":
                        item.Details = results.ToObject<MiniPet>();
                        break;
                    case "Tool":
                        item.Details = results.ToObject<Tool>();
                        break;
                    case "Trinket":
                        item.Details = results.ToObject<Trinket>();
                        break;
                    case "UpgradeComponent":
                        item.Details = results.ToObject<UpgradeComponent>();
                        break;
                    case "Weapon":
                        item.Details = results.ToObject<Weapon>();
                        break;
                    default:
                        break;
                }
                return item;
            }
        }

        public static string Adress = "https://api.guildwars2.com/v2/items/";
    }

    public struct Item
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "chat_link")]
        public string ChatLink { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public int Level { get; set; }
        [JsonProperty(PropertyName = "vendor_value")]
        public int VendorValue { get; set; }
        [JsonProperty(PropertyName = "default_skin")]
        public int DefaultSkinId { get; set; }
        public string[] Flags { get; set; }
        [JsonProperty(PropertyName = "game_types")]
        public string[] GameTypes { get; set; }
        public string[] Restrictions { get; set; }
        public object Details { get; set; } //
    }

    public struct Armor
    {
        [JsonProperty(PropertyName = "type")]
        public string ArmorType { get; set; }
        [JsonProperty(PropertyName = "weight_class")]
        public string WeightClass { get; set; }
        public int Defense { get; set; }
        [JsonProperty(PropertyName = "infusion_slots")]
        public InfusionSlot[] InfusionSlots { get; set; }
        [JsonProperty(PropertyName = "infix_ugrade")]
        public InfixUpgrade InfixUpgrade { get; set; }
        [JsonProperty(PropertyName = "suffix_item_id")]
        public int SuffixItemID { get; set; }
        [JsonProperty(PropertyName = "secondary_suffix_item_id")]
        public string SecondarySuffixItemID { get; set; }
        [JsonProperty(PropertyName = "stat_choices")]
        public string[] StatChoices { get; set; }
    }

    public struct Back
    {
        [JsonProperty(PropertyName = "infusion_slots")]
        public InfusionSlot[] InfusionSlots { get; set; }
        [JsonProperty(PropertyName = "infix_upgrade")]
        public InfixUpgrade InfixUpgrade { get; set; }
        [JsonProperty(PropertyName = "suffix_item_id")]
        public int SuffixItemID { get; set; }
        [JsonProperty(PropertyName = "secondary_suffix_item_id")]
        public string SecondarySuffixItemID { get; set; }
        [JsonProperty(PropertyName = "stat_choices")]
        public string[] StatChoices { get; set; }
    }

    public struct Bag
    {
        public int Size { get; set; }
        [JsonProperty(PropertyName = "no_sell_or_sort")]
        public bool InvisibleOrSafe { get; set; }
    }

    public struct Consumable
    {
        [JsonProperty(PropertyName = "type")]
        public string ConsumableType { get; set; }
        public string Description { get; set; }
        [JsonProperty(PropertyName = "duration_ms")]
        public int DurationMS { get; set; }
        [JsonProperty(PropertyName = "unlock_type")]
        public string UnlockType { get; set; }
        [JsonProperty(PropertyName = "color_id")]
        public int ColorID { get; set; }
        [JsonProperty(PropertyName = "recipe_id")]
        public int RecipeID { get; set; }
        [JsonProperty(PropertyName = "apply_count")]
        public int ApplyCount { get; set; }
        public string Name { get; set; }
        public int[] Skins { get; set; }
    }

    public struct Container
    {
        [JsonProperty(PropertyName = "type")]
        public string ContainerType { get; set; }
    }

    public struct Gathering
    {
        [JsonProperty(PropertyName = "type")]
        public string ToolType { get; set; }
    }

    public struct Gizmo
    {
        [JsonProperty(PropertyName = "type")]
        public string GizmoType { get; set; }
    }

    public struct MiniPet
    {
        [JsonProperty(PropertyName = "minipet_id")]
        public int MinipetID { get; set; }
    }

    public struct Tool
    {
        [JsonProperty(PropertyName = "type")]
        public string SalvageKitType { get; set; }
        public int Charges { get; set; }
    }

    public struct Trinket
    {
        [JsonProperty(PropertyName = "type")]
        public string TrinketType { get; set; }
        [JsonProperty(PropertyName = "infusion_slots")]
        public InfusionSlot[] InfusionSlots { get; set; }
        [JsonProperty(PropertyName = "infix_upgrade")]
        public InfixUpgrade InfixUpgrade { get; set; }
        [JsonProperty(PropertyName = "suffix_item_id")]
        public int SuffixItemID { get; set; }
        [JsonProperty(PropertyName = "secondary_suffix_item_id")]
        public string SecondarySuffixItemID { get; set; }
        [JsonProperty(PropertyName = "stat_choices")]
        public string[] StatChoices { get; set; }
    }

    public struct UpgradeComponent
    {
        [JsonProperty(PropertyName = "type")]
        public string UpgradeType { get; set; }
        public string[] Flags { get; set; }
        [JsonProperty(PropertyName = "infusion_upgrade_flags")]
        public string[] InfusionUpgradeFlags { get; set; }
        public string Suffix { get; set; }
        [JsonProperty(PropertyName = "infix_upgrade")]
        public InfixUpgrade InfixUpgrade { get; set; }
        public string[] Bonuses { get; set; }
    }

    public struct Weapon
    {
        [JsonProperty(PropertyName = "type")]
        public string WeaponType { get; set; }
        [JsonProperty(PropertyName = "damage_type")]
        public string DamageType { get; set; }
        [JsonProperty(PropertyName = "min_power")]
        public int MinPower { get; set; }
        [JsonProperty(PropertyName = "max_power")]
        public int MaxPower { get; set; }
        public int Defense { get; set; }
        [JsonProperty(PropertyName = "infusion_slots")]
        public InfusionSlot[] InfusionSlot { get; set; }
        [JsonProperty(PropertyName = "infix_upgrade")]
        public InfixUpgrade InfixUpgrade { get; set; }
        [JsonProperty(PropertyName = "suffix_item_id")]
        public int SuffixItemID { get; set; }
        [JsonProperty(PropertyName = "secondary_suffix_item_id")]
        public string SecondarySuffixItemID { get; set; }
        [JsonProperty(PropertyName = "stat_choices")]
        public string[] StatChoices { get; set; }
    }

    public struct InfusionSlot
    {
        public string[] Flags { get; set; }
        [JsonProperty(PropertyName = "item_id")]
        public int? ItemID { get; set; }
    }

    public struct InfixUpgrade
    {
        public AttributeStruct[] Attributes { get; set; }
        public Buff? Buff { get; set; }
    }

    public struct AttributeStruct
    {
        public string Attribute { get; set; }
        public int Modifier { get; set; }
    }

    public struct Buff
    {
        [JsonProperty(PropertyName = "skill_id")]
        public int SkillID { get; set; }
        public string Description { get; set; }
    }
}
