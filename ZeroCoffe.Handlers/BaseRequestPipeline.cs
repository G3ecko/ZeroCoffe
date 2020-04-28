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
        private List<IRequestHandler> preHanlders;
        private List<IRequestHandler> Handlers;

        public BaseRequestPipeline()
        {
            preHanlders = new List<IRequestHandler>();
            Handlers = new List<IRequestHandler>();


        }
        public void AddNewHandler(IRequestHandler handler, HandlerType handlerType)
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
            await ExecHandlers(request, preHanlders, resList, Context);

            if (resList.Any(o => o.AnyError))
                return resList;

            resList.Clear();

            await ExecHandlers(request, Handlers, resList, Context);
            if (resList.Any(o => o.AnyError))
                return resList;

            return resList;
        }

        private async Task ExecHandlers(IRequest request, List<IRequestHandler> Handlers, List<IResponse> resList, Dictionary<string, object> Context)
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
