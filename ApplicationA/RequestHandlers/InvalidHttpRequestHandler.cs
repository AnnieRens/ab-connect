using System;
using System.Net;
using System.Text;

namespace ApplicationA.RequestHandlers
{
    public class InvalidHttpRequestHandler : IHttpRequestHandler
    {
        public const string Name = "/InvalidWebRequestHandler";

        public void Handle(HttpListenerContext context)
        { 
            var serverResponse = context.Response;
            serverResponse.StatusCode = (int)HttpStatusCode.NotFound;

            var message = "Could not find resource.";
            var messageBytes = Encoding.Default.GetBytes(message);
            serverResponse.OutputStream.Write(messageBytes, 0, messageBytes.Length);

            serverResponse.Close();

            Console.WriteLine("Invalid request "+ context.Request.RawUrl);
        }

        public string GetName()
        {
            return Name;
        }
    }
}