namespace Utils
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;
    using Utils.Commands;

    public class MessageBroker
    {
        public TcpClient Client { get; set; }

        public NetworkStream Stream = null;

        public void SendMessage(ICommand cmd)
        {
            var binFormatter = new BinaryFormatter();

            if (Client != null)
            {
                try
                {
                    Stream = Client.GetStream();
                    binFormatter.Serialize(Stream, cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось отправить сообщение: {ex.Message}");
                }
            }
        }

        public ICommand ReceiveMessage()
        {
            var binFormatter = new BinaryFormatter();
            ICommand cmd = null;

            try
            {
                NetworkStream stream = Client.GetStream();
                if (stream.DataAvailable)
                    cmd = (ICommand)binFormatter.Deserialize(stream);
            }
            catch (Exception)
            {
                return null;
            }

            return cmd;
        }

        public ICommand PackFile(string fileName)
        {
            var bytesToSend = File.ReadAllBytes(fileName);
            var fileLen = bytesToSend.Length;

            return new FileExchangeCommand(fileName, fileLen, bytesToSend);
        }

        //public FileExchangeCommand UnpackFile(string fileName)
        //{
        //    var bytesToSend = File.ReadAllBytes(fileName);
        //    var fileLen = bytesToSend.Length;

        //    return new FileExchangeCommand(fileName, fileLen, bytesToSend);
        //}

        public void SendSimpleMessage(string msg)
        {
            var binFormatter = new BinaryFormatter();

            if (Client != null)
            {
                try
                {
                    Stream = Client.GetStream();
                    binFormatter.Serialize(Stream, msg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось отправить сообщение: {ex.Message}");
                }
            }
        }

        public string ReceiveSimpleMessage()
        {
            var binFormatter = new BinaryFormatter();
            string message = string.Empty;

            try
            {
                NetworkStream stream = Client.GetStream();
                message = (string)binFormatter.Deserialize(stream);
            }
            catch (Exception)
            {

            }

            return message;
        }
    }
}