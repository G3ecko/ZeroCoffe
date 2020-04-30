using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Common;
using ZeroCoffe.Handlers.Interface;

public abstract class Handler<TRequest, TResponse> : IRequestHandler
    where TRequest : IRequest
{
    public Task<IResponse> RequestHandle(IRequest request, Dictionary<string, object> Context)
    {

       return  Task.FromResult((IResponse)Handle((TRequest)request, Context).GetAwaiter().GetResult());
    }

    public abstract Task<TResponse> Handle(TRequest response, Dictionary<string, object> Context);

    public Type GetRequestType() => typeof(TRequest);
    
}
