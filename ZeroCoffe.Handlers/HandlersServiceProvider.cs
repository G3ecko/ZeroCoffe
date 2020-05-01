using System;
using System.Linq;
using System.Collections.Generic;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers
{
    public class HandlersServiceProvider : IHandlersServiceProvider
    {
        public readonly List<IBaseHandler> Handlers;

        public HandlersServiceProvider()
        {
            Handlers = new List<IBaseHandler>();
        }
        public void Register<TRequest>(IBaseHandler requestHandler) where TRequest : IRequest
        {
            if (requestHandler == null)
            {
                throw new ArgumentNullException($"{nameof(requestHandler)} is null");
            }
            if (typeof(TRequest) != requestHandler.GetRequestType())
            {
                throw new ArgumentException($"Handler does't implment response");
            }

            Handlers.Add(requestHandler);
        }

        public List<IBaseHandler> GetHandlers(IRequest request) => this.Handlers.Where(o => o.GetRequestType() == request.GetType()).ToList();
    }
}