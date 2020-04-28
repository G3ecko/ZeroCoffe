using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCoffe.Handlers.Common
{
    public class HandlersSample1 : BaseHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, Dictionary<string, object> Context)
        {
           
            return Task.FromResult(new TestResponse1() { text = response.text });
        }
    }

    public class HandlersSample2 : BaseHandler<TestRequest2, TestResponse2>
    {
        public override Task<TestResponse2> Handle(TestRequest2 response, Dictionary<string, object> Context)
        {
            throw new NotImplementedException();
        }
    }

    public class HandlersSampleWithError : BasePreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, Dictionary<string, object> Context)
        {
            return Task.FromResult(new TestResponse1() { AnyError = true });
        }
    }

    public class HandlersSampleNOError : BasePreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, Dictionary<string, object> Context)
        {
            Context.Add("TEST", 1);
            return Task.FromResult(new TestResponse1() { AnyError = false });
        }
    }

   
}
