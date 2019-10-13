using System.Net;
using System.Text;
using ApplicationA.Services;
using Newtonsoft.Json;

namespace ApplicationA.RequestHandlers
{
    public class SendMessageHttpRequestHandler : IHttpRequestHandler
    {
        public const string Name = "/message";

        public void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            var statusCode = (int) HttpStatusCode.OK;
            var message = "";

            using (var body = context.Request.InputStream)
            {
                using (var reader = new System.IO.StreamReader(body, context.Request.ContentEncoding))
                {
                    var data = reader.ReadToEnd();
                    var messageText = JsonConvert.DeserializeObject<string>(data);
                    
                    var messageService = new MessagesService();
                    var result = messageService.SendMessage(messageText);

                    if (!result.IsSucceed)
                    {
                        statusCode = (int) HttpStatusCode.InternalServerError;
                        message = result.ErrorMessage;
                    }
                }
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