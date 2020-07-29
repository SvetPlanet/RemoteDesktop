namespace Utils.Commands
{
    using System;

    [Serializable]
    public class DisconnectClientCommand: ICommand
    {
        private string _ipAdr = string.Empty;

        public DisconnectClientCommand(string ip)
        {
            _ipAdr = ip;
        }

        public object[] GetData()
        {
            throw new NotImplementedException();
        }
    }
}
