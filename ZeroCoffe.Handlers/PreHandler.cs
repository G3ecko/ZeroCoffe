using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Interface;

public abstract class PreHandler<TRequest, TResponse> : IPreHandlerRequest
    where TRequest : IRequest
{
    public Task<IResponse> RequestHandle(IRequest request, Dictionary<string, object> Context)
    {

        return Task.FromResult((IResponse)Handle((TRequest)request, Context).GetAwaiter().GetResult());
    }

    public virtual Task<TResponse> Handle(TRequest response, Dictionary<string, object> Context)
    {
        return Task.FromResult((TResponse)default);
    }

    public Type GetRequestType() => typeof(TRequest);
}
