namespace Host
{
    using System.Windows.Forms;
    using AxRDPCOMAPILib;
    public enum Status
    {
        Empty,
        Busy
    }
    public class Display
    {
        public int Id { get; private set; }
        public AxRDPViewer RdpDisplay { get; set; }
        public Status State { get; set; }
        public int ConnectionId { get; set; }
        public Label Label { get; set; }
        public Display(int id, AxRDPViewer rdpDisplay, int connectionId, Label label, Status state = Status.Busy)
        {
            this.Id = id;
            this.RdpDisplay = rdpDisplay;
            this.ConnectionId = connectionId;
            this.State = state;
            this.Label = label;
        }
        public Display(int id, AxRDPViewer rdpDisplay, Label label, Status state = Status.Empty)
        {
            this.Id = id;
            this.RdpDisplay = rdpDisplay;
            this.Label = label;
            this.State = state;
        }
        public void Reserve(int connectionId)
        {
            this.ConnectionId = connectionId;
            this.State = Status.Busy;
        }
        public void Unreserve(int connectionId)
        {
            this.ConnectionId = 0;
            this.State = Status.Empty;
        }
    }
}
