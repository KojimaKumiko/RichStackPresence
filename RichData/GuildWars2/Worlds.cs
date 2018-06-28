using Newtonsoft.Json;
using System.Net;

namespace RichData.GuildWars2
{
    public class Worlds
    {
        public WorldData GetWorld(int ID)
        {
            using(var webClient = new WebClient())
            {
                var json = webClient.DownloadString(WorldData.Address + "?id=" + ID);
                return JsonConvert.DeserializeObject<WorldData>(json);
            }
        }
    }

    public struct WorldData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Population { get; set; }
        public static string Address = "https://api.guildwars2.com/v2/worlds";
    }
}
