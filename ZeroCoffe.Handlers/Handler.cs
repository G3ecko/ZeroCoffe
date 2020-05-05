using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Common;
using ZeroCoffe.Handlers.Interface;

public abstract class Handler<TRequest, TResponse> : IRequestHandler
    where TRequest : IRequest
{
    public Task<IResponse> RequestHandle(IRequest request, IDictionary<string, object> Context)
    {
        var res = (IResponse)Handle((TRequest)request, Context).GetAwaiter().GetResult();
        res.HandledBy = typeof(IRequestHandler).Name;
        return Task.FromResult(res);
    }

    public abstract Task<TResponse> Handle(TRequest response, IDictionary<string, object> Context);

    public Type GetRequestType() => typeof(TRequest);

}
