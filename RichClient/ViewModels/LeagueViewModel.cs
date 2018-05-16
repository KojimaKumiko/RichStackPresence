using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public sealed class LeagueViewModel : Screen, IMainViewTabItem
    {
        public LeagueViewModel()
        {
            DisplayName = "League of Legends";
        }
    }
}
