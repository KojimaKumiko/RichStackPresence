using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData.GuildWars2
{
    public class Items
    {
    }

    public struct Item
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "chat_link")]
        public string ChatLink { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // not sure how to resolve this completly
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
    }
}
