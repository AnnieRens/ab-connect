using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ApplicationB
{
    class Program
    {
        private static readonly int _port = 8005;
        private static readonly string _address = "127.0.0.1";

        static void Main(string[] args)
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse(_address), _port);
            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                Console.WriteLine("Application B started. Waiting for connection...");

                while (true)
                {
                    var handler = listenSocket.Accept();
                    var builder = new StringBuilder();
                    var data = new byte[256];

                    do
                    {
                        var bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder);

                    //var message = "message sent";
                    //data = Encoding.Unicode.GetBytes(message);
                    //handler.Send(data);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}