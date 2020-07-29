namespace RemoteClient
{
    public class Settings
    {
        public string HostIp { get; set; }

        public int HostPort { get; set; }

        public Settings()
        {
            HostIp = Properties.Settings.Default.HostIp;

            HostPort = Properties.Settings.Default.HostPort;
        }

        public void UpdateSettings()
        {
            HostIp = Properties.Settings.Default.HostIp;

            HostPort = (int)Properties.Settings.Default.HostPort;
        }

        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(HostIp) || HostPort <= 0)
                return true;

            return false;
        }
    }
}
