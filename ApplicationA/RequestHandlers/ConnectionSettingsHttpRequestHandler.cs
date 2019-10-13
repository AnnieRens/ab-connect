using System;
using System.Net;
using System.Text;
using ApplicationA.Services;
using Newtonsoft.Json;

namespace ApplicationA.RequestHandlers
{
    public class ConnectionSettingsHttpRequestHandler : IHttpRequestHandler
    {
        public const string Name = "/connection-settings";

        public void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            var statusCode = (int)HttpStatusCode.NoContent;
            var message = "";

            try
            {
                using (var body = context.Request.InputStream)
                {
                    using (var reader = new System.IO.StreamReader(body, context.Request.ContentEncoding))
                    {
                        var data = reader.ReadToEnd();
                        var connectionSettings = JsonConvert.DeserializeObject<ConnectionSettingsModel>(data);

                        var connectionSettingsService = new ConnectionSettingsService();
                        connectionSettingsService.Update(connectionSettings.Host, connectionSettings.Port);
                    }
                }
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