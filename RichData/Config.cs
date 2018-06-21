using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichData
{
    public class Config
    {
        [JsonProperty(PropertyName = "Key")]
        public string Key { get; private set; }

        /// <summary>
        /// Loads a Config JSON from the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Config LoadFromFile(string path)
        {
            using (var sr = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
            }
        }

        /// <summary>
        /// Saves a config JSON to the specified path    
        /// </summary>
        /// <param name="path"></param>
        public void SaveToFile(string path, string directory)
        {
            Directory.CreateDirectory(directory);
            using (var sw = new StreamWriter(path))
            {
                sw.Write(JsonConvert.SerializeObject(this));
            }
        }
    }
}
