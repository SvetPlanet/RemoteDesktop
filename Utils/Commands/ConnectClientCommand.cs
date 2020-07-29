using System;
using Utils.Models;

namespace Utils.Commands
{
    [Serializable]
    public class ConnectClientCommand : ICommand
    {
        public User user { get; set; }

        public string connectionString { get; set; }

        public bool prohibitDemonstration { get; set; }

        public ConnectClientCommand(User user, string connectionString, bool prohibitDemonstration)
        {
            this.user = user;
            this.connectionString = connectionString;
            this.prohibitDemonstration = prohibitDemonstration;
        }

        public ConnectClientCommand(User user, string connectionString)
        {
            this.user = user;
            this.connectionString = connectionString;
        }

        public object[] GetData()
        {
            return new object[] { user, connectionString, prohibitDemonstration };
        }
    }
}
