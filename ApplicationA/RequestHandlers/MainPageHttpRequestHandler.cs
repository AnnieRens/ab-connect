using System.IO;
using System.Net;
using System.Text;

namespace ApplicationA.RequestHandlers
{
    public class MainPageHttpRequestHandler : IHttpRequestHandler
    {
        public const string Name = "/";

        public void Handle(HttpListenerContext context)
        {
            var response = context.Response;
            response.StatusCode = (int) HttpStatusCode.OK;

            var html = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + "/Index.html"));

            var messageBytes = Encoding.Default.GetBytes(html);
            response.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            response.Close();
        }

        public string GetName()
        {
            return Name;
        }
    }
}