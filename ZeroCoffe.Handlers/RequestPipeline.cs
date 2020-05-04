using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Common;
using ZeroCoffe.Handlers.Interface;


namespace ZeroCoffe.Handlers
{
    public class RequestPipeline : IRequestPipeline
    {
        private readonly IHandlersServiceProvider provider;

        public RequestPipeline(IHandlersServiceProvider  provider = null)
        {
            this.provider = provider ?? new HandlersServiceProvider();
        }

        public async Task<List<IResponse>> ExecPipeline(IRequest request)
        {
            Dictionary<string, object> Context = new Dictionary<string, object>();
            var resList = new List<IResponse>();
            var handlers = this.provider.GetHandlers(request);

            var preHandlers = FilterHandlers(handlers,(o) => o.GetType().GetInterface(nameof(IPreHandlerRequest)) != null);

            await ExecHandlers(request, preHandlers, resList, Context);

            if (resList.Any(o => o.AnyError))
                return resList;

            resList.Clear();
            var Handlers = FilterHandlers(handlers,(o) => o.GetType().GetInterface(nameof(IRequestHandler)) != null);

            await ExecHandlers(request, Handlers, resList, Context);
            if (resList.Any(o => o.AnyError))
                return resList;

            return resList;
        }

        public List<IBaseHandler> FilterHandlers(List<IBaseHandler> handlers, Func<IBaseHandler,bool> filter) 
                        => (handlers.Where(filter).ToList());

        private async Task ExecHandlers(IRequest request, List<IBaseHandler> Handlers, List<IResponse> resList, Dictionary<string, object> Context)
        {
            if (Handlers != null && Handlers.Count > 0)
            {
                foreach (var handler in Handlers)
                {
                    try
                    {
                        var res = await handler.RequestHandle(request, Context);
                        resList.Add(res);
                        if (res.AnyError)
                            break;

                    }
                    catch
                    {
                        resList.Add(new DefaultResponseError(true, $"Erro processing handler {handler.GetType()}"));
                        break;
                    }
                }
            }
        }
    }
}
