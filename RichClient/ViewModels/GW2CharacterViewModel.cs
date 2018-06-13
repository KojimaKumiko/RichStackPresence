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
            Age = SelectedCharacter.Age / 3600;
            CreationTime = DateTime.Parse(SelectedCharacter.Created, null, System.Globalization.DateTimeStyles.RoundtripKind);
            Level = SelectedCharacter.Level;
            Console.WriteLine(CreationTime);
        }

        private void GetEquipment()
        {
            foreach(var equip in SelectedCharacter.Equipment)
            {

            }
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
        private double? _age;
        public double? Age
        {
            get { return _age; }
            set
            {
                if(value == _age) { return; }
                _age = value;
                NotifyOfPropertyChange(() => Age);
            }
        }
        private DateTime? _creationTime;
        public DateTime? CreationTime
        {
            get { return _creationTime; }
            set
            {
                if(value == _creationTime) { return; }
                _creationTime = value;
                NotifyOfPropertyChange(() => CreationTime);
            }
        }
        private int? _level;
        public int? Level
        {
            get { return _level; }
            set
            {
                if( value == _level) { return; }
                _level = value;
                NotifyOfPropertyChange(() => Level);
            }
        }
    }
}
