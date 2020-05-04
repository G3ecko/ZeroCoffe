using System;
using System.Linq;
using System.Collections.Generic;
using ZeroCoffe.Handlers.Interface;
using System.Collections.Concurrent;

namespace ZeroCoffe.Handlers
{
    public class HandlersServiceProvider : IHandlersServiceProvider
    {
        public readonly ConcurrentDictionary<Type,List<IBaseHandler>> Handlers;

        public HandlersServiceProvider()
        {
            Handlers = new ConcurrentDictionary<Type, List<IBaseHandler>>();
        }
        public void Register<TRequest>(IBaseHandler requestHandler) where TRequest : IRequest
        {
            var requestTye = typeof(TRequest);
            if (requestHandler == null)
            {
                throw new ArgumentNullException($"{nameof(requestHandler)} is null");
            }
            if (typeof(TRequest) != requestHandler.GetRequestType())
            {
                throw new ArgumentException($"Handler does't implment response");
            }

            if (Handlers.ContainsKey(requestTye))
            {
                this.Handlers[requestTye].Add(requestHandler);
            }
            else
            {
                this.Handlers[requestTye] = new List<IBaseHandler>() { requestHandler };
            }

        }

        public List<IBaseHandler> GetHandlers(IRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} is null");
            }

            var requestTye = request.GetType();


            if (Handlers.ContainsKey(requestTye))
            {
                if (this.Handlers.TryGetValue(requestTye, out List<IBaseHandler> result))
                {
                    return result;
                }
            }

            throw new KeyNotFoundException("Handler not registered for this request");
        }
    }
}