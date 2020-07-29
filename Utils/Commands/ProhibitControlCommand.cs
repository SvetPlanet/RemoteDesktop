namespace Utils.Commands
{
    using System;

    [Serializable]
    public class ProhibitControlCommand : ICommand
    {
        private bool prohibitControl;

        public ProhibitControlCommand(bool prohibitControl)
        {
            this.prohibitControl = prohibitControl;
        }

        public object[] GetData()
        {
            return new object[] { prohibitControl };
        }
    }
}
