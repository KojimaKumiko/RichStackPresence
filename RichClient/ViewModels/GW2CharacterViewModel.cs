using Caliburn.Micro;
using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RichClient.ViewModels
{
    public class GW2CharacterViewModel : Screen
    {
        public GW2CharacterViewModel()
        {
            CharacterList = new BindableCollection<string>();
            SelectedCharacter = new Character();
            Refresh();
        }

        public async void OnSelectionChanged(string charName)
        {
            var characters = new Characters();
            SelectedCharacter = await characters.GetCharacterAsync(charName, APIKey);
            Console.WriteLine(SelectedCharacter.Name);
        }

        public BindableCollection<string> CharacterList { get; set; }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                NotifyOfPropertyChange(() => SelectedCharacter);
            }
        }

        public string APIKey { get; set; }
    }
}
