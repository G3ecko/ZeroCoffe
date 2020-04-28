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
        void AddNewHandler(IRequestHandler handler, HandlerType handlerType);
        Task<List<IResponse>> ExecPipeline(IRequest request);
    }
}
