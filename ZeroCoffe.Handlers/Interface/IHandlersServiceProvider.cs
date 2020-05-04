using System.Collections.Generic;

namespace ZeroCoffe.Handlers.Interface
{
    public interface IHandlersServiceProvider
    {   
          void Register<TRequest>(IBaseHandler requestHandler) where TRequest : IRequest;
          List<IBaseHandler> GetHandlers(IRequest request);
    }
}