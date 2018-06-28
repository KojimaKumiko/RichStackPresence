using Caliburn.Micro;
using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RichClient.ViewModels
{
    public sealed class GW2ViewModel : Screen, IMainViewTabItem
    {
        public GW2ViewModel()
        {
            DisplayName = "Guild Wars 2";
            GW2CharViewModel = new GW2CharacterViewModel();
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
                var character = new Characters();
                CharacterListTask = character.GetCharacterListAsync(APIKey);
                GW2CharViewModel.APIKey = APIKey;
            }
        }

        public async void ResolveCharacterList()
        {
            foreach(var character in GW2CharViewModel.CharacterList.ToList())
            {
                GW2CharViewModel.CharacterList.Remove(character);
            }
            var charList = await CharacterListTask;
            for(int i = 0; i < charList.Length; i++)
            {
                var character = charList[i];
                GW2CharViewModel.CharacterList.Add(character);
            }
        }

        public void InitAccViewModel()
        {
            GW2AccViewModel = new GW2AccountViewModel(APIKey);
        }

        public void InitPvPViewModel()
        {
            GW2PvPViewModel = new GW2PvPViewModel(APIKey);
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
        private GW2CharacterViewModel _gw2CharViewModel;
        public GW2CharacterViewModel GW2CharViewModel
        {
            get { return _gw2CharViewModel; }
            set
            {
                _gw2CharViewModel = value;
                NotifyOfPropertyChange(() => GW2CharViewModel);
            }
        }
        private GW2AccountViewModel _gw2AccViewModel;
        public GW2AccountViewModel GW2AccViewModel
        {
            get { return _gw2AccViewModel; }
            set
            {
                if (value == _gw2AccViewModel)
                    return;
                _gw2AccViewModel = value;
                NotifyOfPropertyChange(() => GW2AccViewModel);
            }
        }
        private GW2PvPViewModel _gw2PvPViewModel;
        public GW2PvPViewModel GW2PvPViewModel
        {
            get { return _gw2PvPViewModel; }
            set
            {
                if (value == _gw2PvPViewModel)
                    return;
                _gw2PvPViewModel = value;
                NotifyOfPropertyChange(() => GW2PvPViewModel);
            }
        }
        public Task<string[]> CharacterListTask { get; set; }

        #endregion
    }
}
