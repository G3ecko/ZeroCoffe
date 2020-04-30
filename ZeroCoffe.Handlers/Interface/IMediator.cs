using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZeroCoffe.Handlers.Interface
{
    public interface IMediator
    {
        void Register<TRequest>(IBaseHandler requestHandler) where TRequest : IRequest;
        Task<List<IResponse>> HandleRequest(IRequest request = null);
    }
}
