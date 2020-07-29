using System;

namespace Utils.Commands
{
    [Serializable]
    public class FileExchangeCommand : ICommand
    {
        string FileName { get; set; }

        int Length { get; set; }

        byte[] Content { get; set; }

        public FileExchangeCommand(string fileName, int len, byte[] content)
        {
            FileName = fileName;
            Length = len;
            Content = content;
        }

        public object[] GetData()
        {
            return new object[] { FileName, Length, Content };
        }
    }
}
