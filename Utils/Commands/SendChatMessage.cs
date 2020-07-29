using System;

namespace Utils.Commands
{
    [Serializable]
    public class SendChatMessage : ICommand
    {
        public string Message { get; set; }

        public SendChatMessage(string msg)
        {
            Message = msg;
        }

        public object[] GetData()
        {
            return new object[] { Message };
        }
    }
}
