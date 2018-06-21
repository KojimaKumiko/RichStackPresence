using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData
{
    public class LeagueKey
    {
        public static string GetKey()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            if (!File.Exists(@"Config\LeagueAPIKey.json"))
            {
                new Config().SaveToFile(@"Config\LeagueAPIKey.json", "Config");
                return null;
            }
            else
            {
                return Config.LoadFromFile(@"Config\LeagueAPIKey.json").Key;
            }
        }
    }
}
