using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Common;

namespace ZeroCoffe.Handlers.Interface
{
    public interface IRequestPipeline
    {
        List<IBaseHandler> preHanlders { get; }
        List<IBaseHandler> Handlers { get; }
        void AddNewHandler(IBaseHandler handler, HandlerType handlerType);
        Task<List<IResponse>> ExecPipeline(IRequest request);
        (List<IBaseHandler>, List<IBaseHandler>) FilterHandlersByType(Type type);
    }
}
