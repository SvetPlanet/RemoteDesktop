using RDPCOMAPILib;
using System;
using System.Net.Sockets;
using System.Threading;
using Utils;
using Utils.Commands;
using Utils.Models;

namespace RemoteClient
{
    public class Client : IDisposable
    {
        public TcpClient TcpClient = null;
        public MessageBroker MessageBroker = null;
        public Thread MessageListener;
        public string MyIP
        {
            get
            {
                return CurrentIP.GetLocalIPAddress();
            }
        }
        public int CheckedCount { get; set; }
        public RDPSession RdpSession;
        public string ConnectionString = string.Empty;
        public Settings Settings = null;
        public User User = null;
        public AuthorizationData AuthData = null;
        public bool ListenOn = true;
        public bool Connected = false;
        public delegate void ThreadMethod();
        public Client(Settings settings, User user, AuthorizationData authData, ThreadMethod listening, bool firstRun)
        {
            this.Settings = settings;
            this.User = user;
            this.AuthData = authData;
            if (firstRun)
                RdpSession = new RDPSession();
            ThreadStart threadDelegate = new ThreadStart(listening);
            MessageListener = new Thread(threadDelegate);
        }
        public void ConnectTcp()
        {
            TcpClient = new TcpClient();
            TcpClient.Connect(Settings.HostIp, Settings.HostPort);
            Connected = true;
            MessageBroker = new MessageBroker();
            MessageBroker.Client = TcpClient;
        }
        public void CreateSession(bool fullDesktop, string chosenApp = "")
        {
            if (RdpSession == null)
            {
                RdpSession = new RDPSession();
            }
            RdpSession.OnControlLevelChangeRequest += ChangeControl;
            RdpSession.OnAttendeeConnected += Incoming;
            RdpSession.Open();
            if (!fullDesktop)
            {
                RdpSession.ApplicationFilter.Enabled = true;
                SetAppForSharing(chosenApp);
            }
        }
        public string GetApplicationName(string fileName)
        {
            const string Executable = ".exe";
            return fileName.EndsWith(Executable)
                ? fileName.Substring(0, fileName.Length - Executable.Length)
                : fileName;
        }
        public void ChangeControl(object Client, CTRL_LEVEL level)
        {
            IRDPSRAPIAttendee attendee = (IRDPSRAPIAttendee)Client;
            attendee.ControlLevel = level;
        }
        public void CreateConnectionString()
        {
            IRDPSRAPIInvitation invitation = RdpSession.Invitations.CreateInvitation(AuthData.Name, AuthData.GroupName, AuthData.Password, AuthData.MaxClients);
            ConnectionString = invitation.ConnectionString;
        }
        private static void Incoming(object Client)
        {
            IRDPSRAPIAttendee newClient = (IRDPSRAPIAttendee)Client;
            newClient.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_VIEW;
        }
        public void Disconnect(bool sendMsgToServer)
        {
            if (TcpClient != null)
            {
                MessageBroker.SendMessage(new DisconnectClientCommand(MyIP));
            }
        }
        public void SetAppForSharing(string chosenApp)
        {
            foreach (RDPSRAPIApplication application in RdpSession.ApplicationFilter.Applications)
            {
                application.Shared = GetApplicationName(application.Name) == chosenApp;
            }
        }
        public void Dispose()
        {
            ConnectionString = string.Empty;
            Connected = false;
            ListenOn = false;
            try
            {
                if (RdpSession != null)
                {
                    foreach (IRDPSRAPIAttendee attendees in RdpSession.Attendees)
                    {
                        attendees.TerminateConnection();
                    }

                    RdpSession.Close();
                    RdpSession = null;
                }
            }
            catch { }
            try
            {
                TcpClient.GetStream().Close();
            }
            catch { }
            MessageBroker = null;
            TcpClient.Close();
            TcpClient = null;
            Settings = null;
            User = null;
            AuthData = null;
            MessageListener = null;
        }
        public void SendConnectionCommand()
        {
            MessageBroker.SendMessage(new ConnectClientCommand(User, ConnectionString, AuthData.ProhibitDemonstration));
        }
    }
}