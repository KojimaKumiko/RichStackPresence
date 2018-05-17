using Caliburn.Micro;
using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RichClient.ViewModels
{
    public sealed class GW2ViewModel : Screen, IMainViewTabItem
    {
        public GW2ViewModel()
        {
            DisplayName = "Guild Wars 2";
            IsDataVisible = false;
            IsKeyVisible = true;
        }

        public void CheckAPIKey()
        {
            // TODO: Add "real" API check here
            if (!string.IsNullOrWhiteSpace(APIKey))
            {
                IsDataVisible = true;
                IsKeyVisible = false;
                var auth = new Authenticated();
            }
        }

        #region Properties
        private string _APIKey;
        public string APIKey
        {
            get { return _APIKey; }
            set
            {
                _APIKey = value;
                NotifyOfPropertyChange(() => APIKey);
            }
        }
        private bool _isDataVisible;
        public bool IsDataVisible
        {
            get { return _isDataVisible; }
            set
            {
                _isDataVisible = value;
                NotifyOfPropertyChange(() => IsDataVisible);
            }
        }
        private bool _isKeyVisible;
        public bool IsKeyVisible
        {
            get { return _isKeyVisible; }
            set
            {
                _isKeyVisible = value;
                NotifyOfPropertyChange(() => IsKeyVisible);
            }
        }
        #endregion
    }
}
