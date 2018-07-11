using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RichData.GuildWars2
{
    public class WvW
    {
        public WvW(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string[] GetWvWMatchIds()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/matches");
                return JsonConvert.DeserializeObject<string[]>(json);
            }
        }

        public WvWMatch GetWvWMatch(string id)
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/matches/" + id);
                return JsonConvert.DeserializeObject<WvWMatch>(json);
            }
        }

        public WvWMatch GetWvWMatchByWorld(int ID)
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/matches?world=" + ID.ToString());
                return JsonConvert.DeserializeObject<WvWMatch>(json);
            }
        }

        public int[] GetWvWAbilityIds()
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/abilities");
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public WvWAbilities GetWvWAbility(int ID)
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/abilities/" + ID.ToString());
                return JsonConvert.DeserializeObject<WvWAbilities>(json);
            }
        }

        public string[] GetWvWObjectivesIds()
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/objectives");
                return JsonConvert.DeserializeObject<string[]>(json);
            }
        }

        public WvWObjectives GetWvWObjective(string id)
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/objectives/" + id);
                return JsonConvert.DeserializeObject<WvWObjectives>(json);
            }
        }

        public int[] GetWvWRankIds()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/ranks");
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public WvWRanks GetWvWRank(int ID)
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/ranks/" + ID.ToString());
                return JsonConvert.DeserializeObject<WvWRanks>(json);
            }
        }

        public int[] GetWvWUpgradeIds()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/upgrades/");
                return JsonConvert.DeserializeObject<int[]>(json);
            }
        }

        public WvWUpgrades GetWvWUpgrade(int ID)
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/wvw/ranks/" + ID.ToString());
                return JsonConvert.DeserializeObject<WvWUpgrades>(json);
            }
        }

        private string _apiKey;
    }

    public struct WvWMatch
    {
        public string ID { get; set; }
        [JsonProperty(PropertyName = "start_time")]
        public string StartTime { get; set; }
        [JsonProperty(PropertyName = "end_time")]
        public string EndTime { get; set; }
        public WvWTeams Scores { get; set; }
        public WvWTeams Worlds { get; set; }
        [JsonProperty(PropertyName = "all_worlds")]
        public WvWAllWorlds AllWorlds { get; set; }
        public WvWTeams Deaths { get; set; }
        public WvWTeams Kills { get; set; }
        [JsonProperty(PropertyName = "victory_points")]
        public WvWTeams VictoryPoints { get; set; }
        public WvWMaps[] Maps { get; set; }
        public Skirmishes[] Skirmishes { get; set; }
    }

    public struct WvWTeams
    {
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Green { get; set; }
    }

    public struct WvWAllWorlds
    {
        public int[] Red { get; set; }
        public int[] Blue { get; set; }
        public int[] Green { get; set; }
    }

    public struct WvWMaps
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public WvWTeams Scores { get; set; }
        public WvWTeams Kills { get; set; }
        public WvWTeams Deaths { get; set; }
        public WvWMapObjectives[] Objectives { get; set; }
        public Bonuses[] Bonuses { get; set; }
    }

    public struct WvWMapObjectives
    {
        public string ID { get; set; }
        public string Owner { get; set; }
        [JsonProperty(PropertyName = "last_flipped")]
        public string LastFlipped { get; set; }
        [JsonProperty(PropertyName = "claimed_by")]
        public string ClaimedBy { get; set; }
        [JsonProperty(PropertyName = "claimed_at")]
        public string ClaimedAt { get; set; }
        [JsonProperty(PropertyName = "points_tick")]
        public int PointsTick { get; set; }
        [JsonProperty(PropertyName = "points_capture")]
        public int PointsCaputre { get; set; }
        [JsonProperty(PropertyName = "guild_upgrades")]
        public int[] GuildUpgrades { get; set; }
        [JsonProperty(PropertyName = "yaks_delivered")]
        public int YaksDelivered { get; set; }
    }

    public struct Bonuses
    {
        public string Type { get; set; }
        public string Owner { get; set; }
    }

    public struct Skirmishes
    {
        public int ID { get; set; }
        public WvWTeams Scores { get; set; }
        [JsonProperty(PropertyName = "map_scores")]
        public MapScores[] MapScores { get; set; }
    }

    public struct MapScores
    {
        public string Type { get; set; }
        public WvWTeams Scores { get; set; }
    }

    public struct WvWAbilities
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public Ranks[] Ranks { get; set; }
    }

    public struct Ranks
    {
        public int Cost { get; set; }
        public string Effect { get; set; }
    }

    public struct WvWRanks
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [JsonProperty(PropertyName = "min_rank")]
        public int MinRank { get; set; }
    }

    public struct WvWObjectives
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [JsonProperty(PropertyName = "sector_id")]
        public int SectorID { get; set; }
        [JsonProperty(PropertyName = "map_id")]
        public int MapID { get; set; }
        [JsonProperty(PropertyName = "map_type")]
        public string MapType { get; set; }
        public int[] Coord { get; set; }
        [JsonProperty(PropertyName = "label_coord")]
        public int[] LabelCoord { get; set; }
        public string Marker { get; set; }
        [JsonProperty(PropertyName = "chat_link")]
        public string ChatLink { get; set; }
        [JsonProperty(PropertyName = "upgrade_id")]
        public int UpgradeID { get; set; }
    }

    public struct WvWUpgrades
    {
        public string ID { get; set; }
        public Tiers[] Tiers { get; set; }
    }

    public struct Tiers
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Upgrades[] Upgrades { get; set; }
    }

    public struct Upgrades
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
