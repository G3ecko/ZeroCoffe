using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Common;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers
{
    public  class RequestPipelineParallel : IRequestPipeline
    {

        private readonly IHandlersServiceProvider provider;

        public RequestPipelineParallel(IHandlersServiceProvider provider = null)
        {
            this.provider = provider ?? new HandlersServiceProvider();
        }

        public async Task<IList<IResponse>> ExecPipeline(IRequest request)
        {
            ConcurrentDictionary<string, object> Context = new ConcurrentDictionary<string, object>();

            var handlers = this.provider.GetHandlers(request);
            var HandlersResult = this.ExecHandlersParallel(request, handlers, Context);

            return await HandlersResult;
        }


        public IList<IBaseHandler> FilterHandlers(List<IBaseHandler> handlers, Func<IBaseHandler, bool> filter)
                         => (handlers.Where(filter).ToList());


        private async Task<List<IResponse>> ExecHandlersParallel(IRequest request, List<IBaseHandler> Handlers, ConcurrentDictionary<string, object> Context)
        {
            ConcurrentBag<IResponse> responses = new ConcurrentBag<IResponse>();
            if (Handlers != null && Handlers.Count > 0)
            {
                var preHandlers = FilterHandlers(Handlers, (o) => o.GetType().GetInterface(nameof(IPreHandlerRequest)) != null);
                var handlers = FilterHandlers(Handlers, (o) => o.GetType().GetInterface(nameof(IRequestHandler)) != null);

                await Task.Run(() => Parallel.ForEach(preHandlers, (prehandler) => ProcessHandler(request, Context, prehandler, responses)));

                if (!responses.Any() || responses.Any(o => !o.AnyError))
                    await Task.Run(() => Parallel.ForEach(handlers, (handler) => ProcessHandler(request, Context, handler, responses)));
            }

            return responses.ToList();
        }

        private static void ProcessHandler(IRequest request, ConcurrentDictionary<string, object> Context, IBaseHandler handler, ConcurrentBag<IResponse> responses)
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
