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
            var charList = await CharacterListTask;
            for(int i = 0; i < charList.Length; i++)
            {
                GW2CharViewModel.CharacterList.Add(charList[i]);
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
        public Task<string[]> CharacterListTask { get; set; }

        #endregion
    }
}
