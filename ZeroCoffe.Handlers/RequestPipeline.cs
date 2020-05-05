using System;
using System.Collections.Concurrent;
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

        public RequestPipeline(IHandlersServiceProvider provider = null)
        {
            this.provider = provider ?? new HandlersServiceProvider();
        }

        public async Task<IList<IResponse>> ExecPipeline(IRequest request)
        {
            Dictionary<string, object> Context = new Dictionary<string, object>();

            var handlers = this.provider.GetHandlers(request);
            var HandlersResult = this.ExecHandlerSequencial(request, handlers, Context);

            return await Task.FromResult(HandlersResult);
        }

        public IList<IBaseHandler> FilterHandlers(List<IBaseHandler> handlers, Func<IBaseHandler, bool> filter)
                        => (handlers.Where(filter).ToList());


        private List<IResponse> ExecHandlerSequencial(IRequest request, List<IBaseHandler> Handlers, Dictionary<string, object> Context)
        {
            List<IResponse> responses = new List<IResponse>();
            if (Handlers != null && Handlers.Count > 0)
            {
                var preHandlers = FilterHandlers(Handlers, (o) => o.GetType().GetInterface(nameof(IPreHandlerRequest)) != null).ToList();
                var handlers = FilterHandlers(Handlers, (o) => o.GetType().GetInterface(nameof(IRequestHandler)) != null).ToList();

                preHandlers.ForEach(prehandler => ProcessHandler(request, Context, prehandler, responses));

                if (!responses.Any() || responses.Any(o => !o.AnyError))
                    handlers.ForEach(handler => ProcessHandler(request, Context, handler, responses));
            }

            return responses.ToList();
        }

        

        private static void ProcessHandler(IRequest request, Dictionary<string, object> Context, IBaseHandler handler, List<IResponse> responses)
        {
            try
            {
                responses.Add(handler.RequestHandle(request, Context).GetAwaiter().GetResult());
            }
            catch (Exception ex)
            {
                responses.Add(new DefaultResponseError(ex.Message));
            }
        }
    }
}
