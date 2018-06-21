using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RichData.GuildWars2
{
    public class Guild
    {
        public GuildData GetGuild(string guildId)
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString("https://api.guildwars2.com/v2/guild/" + guildId);
                return JsonConvert.DeserializeObject<GuildData>(json);
            }
        }
    }

    public struct GuildData
    {
        public int Level { get; set; }
        public string MOTD { get; set; }
        public int Influence { get; set; }
        public string Aetherium { get; set; }
        public int Facor { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
    }
}
