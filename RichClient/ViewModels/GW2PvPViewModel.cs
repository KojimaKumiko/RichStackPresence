using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public class GW2PvPViewModel
    {
        public GW2PvPViewModel(string APIKey)
        {
            _apiKey = APIKey;
            var pvp = new PvP(_apiKey);
            PvPStats = pvp.GetPvPStats();
        }

        private string _apiKey;
        public PvPStats PvPStats { get; set; }
        public PvPRanks PvPRank { get; set; }
    }
}
