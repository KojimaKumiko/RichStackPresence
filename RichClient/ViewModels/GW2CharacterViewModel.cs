using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public class GW2CharacterViewModel : Screen
    {
        public GW2CharacterViewModel()
        {
            CharacterList = new BindableCollection<string>();
        }

        public void SelectedItemChanged()
        {

        }

        public BindableCollection<string> CharacterList { get; set; }
    }
}
