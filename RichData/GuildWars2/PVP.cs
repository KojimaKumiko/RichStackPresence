using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData.GuildWars2
{
    public class PvP
    {
    }

    public struct PvPStats
    {
        [JsonProperty(PropertyName = "pvp_rank")]
        public int PvPRank { get; set; }
        [JsonProperty(PropertyName = "pvp_rank_points")]
        public int PvPRankPoints { get; set; }
        [JsonProperty(PropertyName = "pvp_rank_rollovers")]
        public int PvPRankRollovers { get; set; }
        public Aggregate Aggregate { get; set; }
        public Professions Professions { get; set; }
        public Ladders Ladders { get; set; }
    }

    public struct Professions
    {
        public string Profession { get; set; }
        public Aggregate Aggregate { get; set; }
    }

    public struct Ladders
    {
        public string Ladder { get; set; }
        public Aggregate Aggregate { get; set; }
    }

    public struct Aggregate
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Desertions { get; set; }
        public int Byes { get; set; }
        public int Forfeits { get; set; }
    }

    public struct PvPGames
    {
        public int ID { get; set; }
        [JsonProperty(PropertyName = "map_id")]
        public int MapID { get; set; }
        public string Started { get; set; }
        public string Ended { get; set; }
        public string Result { get; set; }
        public string Team { get; set; }
        public string Profession { get; set; }
        public Scores Scores { get; set; }
        [JsonProperty(PropertyName = "rating_type")]
        public string RatingType { get; set; }
        [JsonProperty(PropertyName = "rating_change")]
        public string RatingChange { get; set; }
        public string Season { get; set; }
    }

    public struct Scores
    {
        public int Red { get; set; }
        public int Blue { get; set; }
    }

    public struct PvPStandings
    {
        public Current[] Current { get; set; }
        public Best[] Best { get; set; }
        [JsonProperty(PropertyName = "season_id")]
        public int SeasonID { get; set; }
    }

    public struct Current
    {
        [JsonProperty(PropertyName = "total_points")]
        public int TotalPoints { get; set; }
        public int Division { get; set; }
        public int Tier { get; set; }
        public int Points { get; set; }
        public int Repeats { get; set; }
        public int Rating { get; set; }
        public int Decay { get; set; }
    }

    public struct Best
    {
        [JsonProperty(PropertyName = "total_points")]
        public int TotalPoints { get; set; }
        public int Division { get; set; }
        public int Tier { get; set; }
        public int Points { get; set; }
        public int Repeats { get; set; }
    }

    public struct PvPRanks
    {
        public int ID { get; set; }
        [JsonProperty(PropertyName = "finisher_id")]
        public int FinishedID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        [JsonProperty(PropertyName = "min_rank")]
        public int MinRank { get; set; }
        [JsonProperty(PropertyName = "max_rank")]
        public int MaxRank { get; set; }
        public Levels[] Levels { get; set; }
    }

    public struct Levels
    {
        [JsonProperty(PropertyName = "min_rank")]
        public int MinRank { get; set; }
        [JsonProperty(PropertyName = "max_rank")]
        public int MaxRank { get; set; }
        public int Points { get; set; }
    }
}
