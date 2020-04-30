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
       
        public readonly IRequestPipeline requestPipeline;

        public Mediator(IRequestPipeline requestPipeline = null)
        {
            this.requestPipeline = requestPipeline ?? new BaseRequestPipeline();
        }

        public void Register<TRequest>(IBaseHandler requestHandler) where TRequest : IRequest
        {
            if (requestHandler == null)
            {
                throw new ArgumentNullException($"requestHandler cannot be null");
            }

            this.RegisterOnPipeline(requestHandler);

        }

        public async Task<List<IResponse>> HandleRequest(IRequest request)
        {
            return await requestPipeline.ExecPipeline(request);
        }

        internal void RegisterOnPipeline(IBaseHandler requestHandler)
        {
            Type requestType = requestHandler.GetType();

            if (requestType.GetInterface("IRequestHandler") != null)
            {
                requestPipeline.AddNewHandler(requestHandler, Common.HandlerType.HANDLE);
            }
            else if (requestType.GetInterface("IPreHandlerRequest") != null)
            {
                requestPipeline.AddNewHandler(requestHandler, Common.HandlerType.PRE_HANDLE);
            }
            else
            {
                throw new ArgumentException("RequestHandler dont have a valid base type");
            }
        }

    }
}
