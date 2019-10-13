using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DatabaseAccess;

namespace ApplicationA.Services
{
    public class MessagesService
    {
        private readonly ConnectionSettingsService _appConnectionSettings;

        public MessagesService()
        {
            _appConnectionSettings = new ConnectionSettingsService();
        }

        public OperationResult SendMessage(string text)
        {
            var settings = _appConnectionSettings.Get();
            var messagesRepository = new MessagesRepository();

            try
            {
                var ipPoint = new IPEndPoint(IPAddress.Parse(settings.Host), settings.Port);
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes(text);
                socket.Send(data);

                data = new byte[256];
                var builder = new StringBuilder();

                do
                {
                    var bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);

                Console.WriteLine("Application B answered: " + builder);

                messagesRepository.Add(new Message(text, DateTime.Now, true));
                messagesRepository.Save();

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                messagesRepository.Add(new Message(text, DateTime.Now, false));
                messagesRepository.Save();

                return OperationResult.Failed(ex.Message);
            }
        }
    }
}