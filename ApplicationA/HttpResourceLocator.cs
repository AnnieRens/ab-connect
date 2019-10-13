using System.Collections.Generic;
using System.Net;
using System.Threading;
using ApplicationA.RequestHandlers;

namespace ApplicationA
{
    public class HttpResourceLocator
    {
        private readonly Dictionary<string, IHttpRequestHandler> _httpRequestHandlers;

        public HttpResourceLocator()
        {
            _httpRequestHandlers = new Dictionary<string, IHttpRequestHandler>();
            AddHttpRequestHandler(new InvalidHttpRequestHandler());
        }

        public void AddHttpRequestHandler(IHttpRequestHandler httpRequestHandler)
        {
            if (!_httpRequestHandlers.ContainsKey(httpRequestHandler.GetName()))
            {
                _httpRequestHandlers.Add(httpRequestHandler.GetName(), httpRequestHandler);
            }
            else
            {
                _httpRequestHandlers[httpRequestHandler.GetName()] = httpRequestHandler;
            }
        }

        public void HandleContext(HttpListenerContext listenerContext)
        {
            var request = listenerContext.Request;
            var requestHandlerName = request.Url.AbsolutePath;

            IHttpRequestHandler handler;

            if (_httpRequestHandlers.ContainsKey(requestHandlerName))
            {
                handler = _httpRequestHandlers[requestHandlerName];
            }
            else
            {
                handler = _httpRequestHandlers[InvalidHttpRequestHandler.Name];
            }

            InvokeHandler(handler, listenerContext);
        }

        private void InvokeHandler(IHttpRequestHandler handler, HttpListenerContext context)
        {
            var handleHttpRequestCommand = new HandleHttpRequestCommand(handler, context);
            var handleHttpRequestThread = new Thread(handleHttpRequestCommand.Execute);
            handleHttpRequestThread.Start();
        }

        public class HandleHttpRequestCommand
        {
            private readonly IHttpRequestHandler _handler;
            private readonly HttpListenerContext _context;

            public HandleHttpRequestCommand(IHttpRequestHandler handler, HttpListenerContext context)
            {
                _handler = handler;
                _context = context;
            }

            public void Execute()
            {
                _handler.Handle(_context);
            }
        }
    }
}