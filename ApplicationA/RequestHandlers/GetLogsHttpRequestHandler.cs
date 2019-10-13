using System;
using System.Net;
using System.Text;
using DatabaseAccess;
using Newtonsoft.Json;

namespace ApplicationA.RequestHandlers
{
    public class GetLogsHttpRequestHandler : IHttpRequestHandler
    {
        public const string Name = "/logs";

        public void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            var statusCode = (int) HttpStatusCode.OK;
            var message = "";

            try
            {
                var messagesRepository = new MessagesRepository();
                var messages = messagesRepository.GetAll();
                message = JsonConvert.SerializeObject(messages);
            }
            catch (Exception e)
            {
                statusCode = (int) HttpStatusCode.InternalServerError;
                message = e.Message;
            }

            response.StatusCode = statusCode;
            var messageBytes = Encoding.Default.GetBytes(message);
            response.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            response.Close();
        }

        public string GetName()
        {
            return Name;
        }
    }
}