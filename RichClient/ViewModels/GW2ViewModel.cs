using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public sealed class GW2ViewModel : Screen, IMainViewTabItem
    {
        public GW2ViewModel()
        {
            DisplayName = "Guild Wars 2";
        }

        public void CheckAPIKey()
        {
            // TODO: Add "real" API check here
            if (!string.IsNullOrWhiteSpace(APIKey))
            {
                //Visibility = "Visible";
            }
            else
            {
                //Visibility = "Collapsed";
            }
        }

        public GW2AccountViewModel Account;
        public string Visibility { get; set; }
        public string APIKey { get; set; }
    }
}
