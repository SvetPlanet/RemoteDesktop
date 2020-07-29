namespace Utils.Models
{
    public class AuthorizationData
    {
        public string Name { get; set; }

        public string GroupName { get; set; }

        public string Password { get; set; }
        
        public int MaxClients { get; set; }

        public bool ProhibitDemonstration { get; set; }
    }
}
