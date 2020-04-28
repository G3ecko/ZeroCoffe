using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZeroCoffe.Handlers.Interface
{
    public interface IMediator
    {
        void Register<TRequest>(IRequestHandler requestHandler);
        IList<IRequestHandler> GetRegisteredRequestHandlers<TRequest>();
        Task<List<IResponse>> HandleRequest<TRequest>(IRequest request = null);
    }
}
