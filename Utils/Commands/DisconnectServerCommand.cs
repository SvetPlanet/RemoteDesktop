using System;

namespace Utils.Commands
{
    [Serializable]
    public class DisconnectServerCommand : ICommand
    {
        private bool disconnect;

        public DisconnectServerCommand(bool d)
        {
            disconnect = d;
        }

        public object[] GetData()
        {
            throw new NotImplementedException();
        }
    }
}
