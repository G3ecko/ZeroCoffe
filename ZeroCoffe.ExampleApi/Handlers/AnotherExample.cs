using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroCoffe.Handlers;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.ExampleApi.Handlers
{
    public class ExampleResponse : BaseResponse
    {
        public ExampleResponse()
        {

        }

        public ExampleResponse(bool anyError)
        {
            base.AnyError = anyError;

        }
        public string textOutput { get; set; }
    }

    public class ExampleRequest: IRequest
    {
        public string text { get; set; }
    }

    public class ValidationExample : PreHandler<ExampleRequest, ExampleResponse>
    {

        public override Task<ExampleResponse> Handle(ExampleRequest response, IDictionary<string, object> Context)
        {
            ExampleResponse exampleResponse = new ExampleResponse();
            exampleResponse.AnyError = (response == null || string.IsNullOrEmpty(response.text));
            return Task.FromResult(exampleResponse);
        }
    }

    public class HandleExample : Handler<ExampleRequest, ExampleResponse>
    {

        public override Task<ExampleResponse> Handle(ExampleRequest response, IDictionary<string, object> Context)
        {
            ExampleResponse exampleResponse = new ExampleResponse();
            exampleResponse.textOutput = response.text;
            return Task.FromResult(exampleResponse);
        }
    }
}
