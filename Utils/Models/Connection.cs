namespace Utils.Models
{
    using AxRDPCOMAPILib;
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Windows.Forms;

    public enum ConnectionState
    {
        Connected,
        Off,
        Demonstration,
        Control,
        Closed
    }

    public class Connection : IComparer<Connection>
    {
        public int ConnectionId { get; set; }

        public string ClientIP { get; set; }

        public User User { get; set; }

        public TcpClient Client { get; set; }

        public string ConnectionString { get; set; }

        public AuthorizationData AuthData { get; set; }

        public MessageBroker MessageBroker { get; set; }

        public bool ProhibitDemo { get; set; }

        public AxRDPViewer RdpViewer { get; set; }

        public TabPage TabPage { get; set; }

        public ConnectionState State { get; set; }

        public int Compare(Connection x, Connection y)
        {
            if (x != null && y != null)
            {
                if (x.ClientIP != null && y.ClientIP != null)
                {
                    return x.ClientIP.CompareTo(y.ClientIP);
                }
            }

            throw new ArgumentNullException();
        }
    }
}
