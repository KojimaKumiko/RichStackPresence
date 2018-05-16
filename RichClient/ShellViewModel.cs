using Caliburn.Micro;
using RichClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient
{
    public class ShellViewModel : Conductor<IMainViewTabItem>.Collection.OneActive
    {
        public ShellViewModel(GW2ViewModel gw2Model, LeagueViewModel leagueModel)
        {
            Items.Add(leagueModel);
            Items.Add(gw2Model);
        }
    }
}
