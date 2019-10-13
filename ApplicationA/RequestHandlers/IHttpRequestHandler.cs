using System.Net;

namespace ApplicationA
{
    public interface IHttpRequestHandler
    {
        void Handle(HttpListenerContext context);

        string GetName();
    }
}