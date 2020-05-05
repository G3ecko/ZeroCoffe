using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Interface;

public abstract class PreHandler<TRequest, TResponse> : IPreHandlerRequest
    where TRequest : IRequest
{
    public Task<IResponse> RequestHandle(IRequest request, IDictionary<string, object> Context)
    {
        var res = (IResponse)Handle((TRequest)request, Context).GetAwaiter().GetResult();
        res.HandledBy = typeof(IPreHandlerRequest).Name;
        return Task.FromResult(res);
    }

    public virtual Task<TResponse> Handle(TRequest response, IDictionary<string, object> Context)
    {
        return Task.FromResult((TResponse)default);
    }

    public Type GetRequestType() => typeof(TRequest);
}
