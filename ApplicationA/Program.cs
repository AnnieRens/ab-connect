using System;
using System.Net;
using System.Threading.Tasks;
using ApplicationA.RequestHandlers;

namespace ApplicationA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = new HttpServer("http://*:12345/");
            server.AddHttpRequestHandler(new MainPageHttpRequestHandler());
            server.AddHttpRequestHandler(new ConnectionSettingsHttpRequestHandler());
            server.AddHttpRequestHandler(new SendMessageHttpRequestHandler());
            server.AddHttpRequestHandler(new GetLogsHttpRequestHandler());

            server.Start();
            Console.ReadKey();
            server.Stop();
        }

        public class HttpServer
        {
            private readonly HttpListener _httpListener;

            private bool _running;

            private readonly HttpResourceLocator _resourceLocator;

            public HttpServer(string prefix)
            {
                if (!HttpListener.IsSupported)
                    throw new NotSupportedException("The Http Program cannot run on this operating system.");

                _httpListener = new HttpListener();
                _httpListener.Prefixes.Add(prefix);
                _resourceLocator = new HttpResourceLocator();
            }

            public void AddHttpRequestHandler(IHttpRequestHandler requestHandler)
            {
                _resourceLocator.AddHttpRequestHandler(requestHandler);
            }

            public void Start()
            {
                if (!_httpListener.IsListening)
                {
                    _httpListener.Start();
                    _running = true;

                    Task.Run(() =>
                    {
                        try
                        {
                            while (_running)
                            {
                                var context = _httpListener.GetContext();
                                _resourceLocator.HandleContext(context);
                            }
                        }
                        catch (HttpListenerException)
                        {
                            Console.WriteLine("HTTP server was shut down.");
                        }
                    });
                }
            }

            public void Stop()
            {
                if (_httpListener.IsListening)
                {
                    _running = false;
                    _httpListener.Stop();
                }
            }
        }
    }
}