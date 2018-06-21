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
            ItemList = new BindableCollection<Item>();
            ArmorList = new BindableCollection<Item>();
            WeaponList = new BindableCollection<Item>();
            SelectedCharacter = new Character();
            Refresh();
        }

        public async void OnSelectionChanged(string charName)
        {
            if (!string.IsNullOrWhiteSpace(charName))
            {
                var characters = new Characters();
                SelectedCharacter = await characters.GetCharacterAsync(charName, APIKey);

                byte[] bytes = Encoding.Default.GetBytes(SelectedCharacter.Name);
                Name = Encoding.UTF8.GetString(bytes);

                Age = SelectedCharacter.Age / 3600;
                CreationTime = DateTime.Parse(SelectedCharacter.Created, null, System.Globalization.DateTimeStyles.RoundtripKind);
                Level = SelectedCharacter.Level;
                var guild = new Guild().GetGuild(SelectedCharacter.Guild);
                GuildName = guild.Name + " | " + guild.Tag;

                bytes = Encoding.Default.GetBytes(GuildName);
                GuildName = Encoding.UTF8.GetString(bytes);

                GetEquipment();
            }
        }

        private void GetEquipment()
        {
            RefreshCollection();
            foreach(var equip in SelectedCharacter.Equipment)
            {
                var items = new Items();
                var item = items.GetItem(equip.Id);
                ItemList.Add(item);
                if(item.Type == "Armor")
                {
                    ArmorList.Add(item);
                }
                else if (item.Type == "Weapon" && equip.Slot == "WeaponA1" || equip.Slot == "WeaponA2" || equip.Slot == "WeaponB1" || equip.Slot == "WeaponB2")
                {
                    WeaponList.Add(item);
                }
            }
            SortArmorList();
        }

        private void SortArmorList()
        {
            var helm = ArmorList.Where(item => ((Armor)item.Details).ArmorType == "Helm").Single();
            var shoulder = ArmorList.Where(item => ((Armor)item.Details).ArmorType == "Shoulders").Single();
            var coat = ArmorList.Where(item => ((Armor)item.Details).ArmorType == "Coat").Single();
            var gloves = ArmorList.Where(item => ((Armor)item.Details).ArmorType == "Gloves").Single();
            var leggings = ArmorList.Where(item => ((Armor)item.Details).ArmorType == "Leggings").Single();
            var boots = ArmorList.Where(item => ((Armor)item.Details).ArmorType == "Boots").Single();

            ArmorList.Move(ArmorList.IndexOf(helm), 0);
            ArmorList.Move(ArmorList.IndexOf(shoulder), 1);
            ArmorList.Move(ArmorList.IndexOf(coat), 2);
            ArmorList.Move(ArmorList.IndexOf(gloves), 3);
            ArmorList.Move(ArmorList.IndexOf(leggings), 4);
            ArmorList.Move(ArmorList.IndexOf(boots), 5);
        }

        private void RefreshCollection()
        {
            foreach(var item in ItemList.ToList())
            {
                ItemList.Remove(item);
            }

            foreach(var item in ArmorList.ToList())
            {
                ArmorList.Remove(item);
            }

            foreach(var item in WeaponList.ToList())
            {
                WeaponList.Remove(item);
            }
        }

        public BindableCollection<string> CharacterList { get; set; }
        public BindableCollection<Item> ItemList { get; set; }
        public BindableCollection<Item> ArmorList { get; set; }
        public BindableCollection<Item> WeaponList { get; set; }

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

        private string _guildName;
        public string GuildName
        {
            get { return _guildName; }
            set
            {
                if(value == _guildName) { return; }
                _guildName = value;
                NotifyOfPropertyChange(() => GuildName);
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(value == _name) { return; }
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
    }
}
