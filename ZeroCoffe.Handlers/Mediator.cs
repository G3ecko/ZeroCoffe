using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers
{

    public class Mediator : IMediator
    {
        public readonly IDictionary<Type, IList<IRequestHandler>> handlers;
        private readonly IRequestPipeline requestPipeline;

        public Mediator(IRequestPipeline requestPipeline = null)
        {
            handlers = new Dictionary<Type, IList<IRequestHandler>>();
            this.requestPipeline = requestPipeline ?? new BaseRequestPipeline();
        }

        public void Register<TRequest>(IRequestHandler requestHandler)
        {
            var requestType = typeof(TRequest);
            if (handlers.ContainsKey(requestType))
            {
                handlers[requestType].Add(requestHandler);
            }
            else
            {
                handlers[requestType] = new List<IRequestHandler>() { requestHandler };
            }
        }

        public IList<IRequestHandler> GetRegisteredRequestHandlers<TRequest>()
        {
            var requestType = typeof(TRequest);
            if (handlers.ContainsKey(requestType))
            {
                return handlers[requestType];
            }
            else
            {
                throw new KeyNotFoundException("No handler registered for this type.");
            }

        }

        public async Task<List<IResponse>> HandleRequest<TRequest>(IRequest request)
        {   
            var registeredHandlers = GetRegisteredRequestHandlers<TRequest>().ToList();
            foreach (var registeredHandler in registeredHandlers)
            {
                requestPipeline.AddNewHandler(registeredHandler, Common.HandlerType.HANDLE);
            }
            return await requestPipeline.ExecPipeline(request);
        }

    }
}
