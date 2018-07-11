using Caliburn.Micro;
using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public class GW2PvPViewModel : Screen
    {
        public GW2PvPViewModel(string APIKey)
        {
            _apiKey = APIKey;
            var pvp = new PvP(_apiKey);
            PvPStats = pvp.GetPvPStats();
            ProfessionDictionary = new ObservableDictionary<Aggregate, string>();
            ProfessionSelected = false;
            FillDictionary();
        }

        private void FillDictionary()
        {
            foreach(var prof in PvPStats.Professions)
            {
                ProfessionDictionary.Add(prof.Value, prof.Key);
            }
        }

        public void ProfessionChanged(KeyValuePair<Aggregate, string> professionPair)
        {
            SelectedProfession = professionPair.Key;
            ProfessionSelected = true;
        }

        private string _apiKey;
        public PvPStats PvPStats { get; set; }
        public PvPRanks PvPRank { get; set; }
        public ObservableDictionary<Aggregate, string> ProfessionDictionary { get; set; }

        private bool _professionSelected;
        public bool ProfessionSelected
        {
            get { return _professionSelected; }
            set
            {
                if (value == _professionSelected)
                    return;
                _professionSelected = value;
                NotifyOfPropertyChange(() => ProfessionSelected);
            }
        }

        private Aggregate _selectedProfession;
        public Aggregate SelectedProfession
        {
            get { return _selectedProfession; }
            set
            {
                _selectedProfession = value;
                NotifyOfPropertyChange(() => SelectedProfession);
            }
        }
    }
}
