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
    public class BaseRequestPipeline : IRequestPipeline
    {
        public List<IBaseHandler> preHanlders { get; private set; }
        public List<IBaseHandler> Handlers { get; private set; }

        public BaseRequestPipeline()
        {
            preHanlders = new List<IBaseHandler>();
            Handlers = new List<IBaseHandler>();
        }

        public void AddNewHandler(IBaseHandler handler, HandlerType handlerType)
        {
            switch (handlerType)
            {

                case HandlerType.HANDLE:
                    Handlers.Add(handler);
                    break;
                case HandlerType.PRE_HANDLE:
                    preHanlders.Add(handler);
                    break;
            }
        }

        public async Task<List<IResponse>> ExecPipeline(IRequest request)
        {
            Dictionary<string, object> Context = new Dictionary<string, object>();
            var resList = new List<IResponse>();
            (var _preHandlers, var _handler) = FilterHandlersByType(request.GetType());
            await ExecHandlers(request, _preHandlers, resList, Context);

            if (resList.Any(o => o.AnyError))
                return resList;

            resList.Clear();

            await ExecHandlers(request, _handler, resList, Context);
            if (resList.Any(o => o.AnyError))
                return resList;

            return resList;
        }

        public (List<IBaseHandler>, List<IBaseHandler>) FilterHandlersByType(Type type) 
                        => (preHanlders.Where(o => o.GetRequestType() == type).ToList(), Handlers.Where(o => o.GetRequestType() == type).ToList());

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
