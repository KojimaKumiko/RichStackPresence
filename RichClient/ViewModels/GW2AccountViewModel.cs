using Caliburn.Micro;
using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichClient.ViewModels
{
    public class GW2AccountViewModel : Screen
    {
        public GW2AccountViewModel(string APIKey)
        {
            this.APIKey = APIKey;
            var auth = new Authenticated(APIKey);
            Account = auth.GetAccount();
            Age = (int.Parse(Account.Age) / 3600).ToString() + " Hours";
            Access = string.Join("; ", Account.Access);
            SetGuilds();
            Created = DateTime.Parse(Account.Created, null, System.Globalization.DateTimeStyles.RoundtripKind);
            SetWorld();
        }

        private void SetGuilds()
        {
            var guild = new Guild();
            foreach(var guildId in Account.Guilds)
            {
                Guilds += guild.GetGuild(guildId).Name + "; ";
            }

            byte[] bytes = Encoding.Default.GetBytes(Guilds);
            Guilds = Encoding.UTF8.GetString(bytes);
        }

        private void SetWorld()
        {
            var world = new Worlds();
            World = world.GetWorld(Account.World).Name;
        }

        public string APIKey { get; set; }

        private Account _account;
        public Account Account
        {
            get { return _account; }
            set
            {
                _account = value;
                NotifyOfPropertyChange(() => Account);
            }
        }

        private string _age;
        public string Age
        {
            get { return _age; }
            set
            {
                if (value == _age)
                    return;
                _age = value;
                NotifyOfPropertyChange(() => Age);
            }
        }

        private string _access;
        public string Access
        {
            get { return _access; }
            set
            {
                if (value == _access)
                    return;
                _access = value;
                NotifyOfPropertyChange(() => Access);
            }
        }

        private string _guilds;
        public string Guilds
        {
            get { return _guilds; }
            set
            {
                if (value == _guilds)
                    return;
                _guilds = value;
                NotifyOfPropertyChange(() => Guilds);
            }
        }

        private DateTime _created;
        public DateTime Created
        {
            get { return _created; }
            set
            {
                if (value == _created)
                    return;
                _created = value;
                NotifyOfPropertyChange(() => Created);
            }
        }

        private string _world;
        public string World
        {
            get { return _world; }
            set
            {
                if (value == _world)
                    return;
                _world = value;
                NotifyOfPropertyChange(() => World);
            }
        }
    }
}
