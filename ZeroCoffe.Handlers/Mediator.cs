using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers
{

    public class Mediator : IMediator
    {
       
        public readonly IRequestPipeline requestPipeline;

        public Mediator(IRequestPipeline requestPipeline = null)
        {
            this.requestPipeline = requestPipeline ?? new RequestPipeline();
        }

        public async Task<IList<IResponse>> HandleRequest(IRequest request)
        {
            return await requestPipeline.ExecPipeline(request);
        }

       
    }
}
