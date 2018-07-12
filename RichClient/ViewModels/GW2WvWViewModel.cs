using Caliburn.Micro;
using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public class GW2WvWViewModel : Screen
    {
        public GW2WvWViewModel(string apiKey)
        {
            var wvw = new WvW(apiKey);
            var auth = new Authenticated(apiKey);
            var world = new Worlds();
            World = world.GetWorld(auth.GetAccount().World);
            Match = wvw.GetWvWMatchByWorld(World.ID);
            Red = world.GetWorld(Match.Worlds.Red).Name;
            Green = world.GetWorld(Match.Worlds.Green).Name;
            Blue = world.GetWorld(Match.Worlds.Blue).Name;

            Start = DateTime.Parse(Match.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            End = DateTime.Parse(Match.EndTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        private DateTime _start;
        public DateTime Start
        {
            get { return _start; }
            set
            {
                if (value == _start)
                    return;
                _start = value;
                NotifyOfPropertyChange(() => Start);
            }
        }

        private DateTime _end;
        public DateTime End
        {
            get { return _end; }
            set
            {
                if (value == _end)
                    return;
                _end = value;
                NotifyOfPropertyChange(() => End);
            }
        }

        private WvWMatch _match;
        public WvWMatch Match
        {
            get { return _match; }
            set
            {
                _match = value;
                NotifyOfPropertyChange(() => Match);
            }
        }

        private WorldData _world;
        public WorldData World
        {
            get { return _world; }
            set
            {
                _world = value;
                NotifyOfPropertyChange(() => World);
            }
        }

        private string _red;
        public string Red
        {
            get { return _red; }
            set
            {
                if (value == _red)
                    return;
                _red = value;
                NotifyOfPropertyChange(() => Red);
            }
        }

        private string _green;
        public string Green
        {
            get { return _green; }
            set
            {
                if (value == _green)
                    return;
                _green = value;
                NotifyOfPropertyChange(() => Green);
            }
        }

        private string _blue;
        public string Blue
        {
            get { return _blue; }
            set
            {
                if (value == _blue)
                    return;
                _blue = value;
                NotifyOfPropertyChange(() => Blue);
            }
        }
    }
}
