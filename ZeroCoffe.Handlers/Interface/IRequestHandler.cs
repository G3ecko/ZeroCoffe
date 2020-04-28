using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Common;

namespace ZeroCoffe.Handlers.Interface
{
    public interface IRequestHandler
    {
        Task<IResponse> RequestHandle(IRequest request, Dictionary<string, object> Context);
    }

}
