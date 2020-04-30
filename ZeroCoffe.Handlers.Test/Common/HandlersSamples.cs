using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCoffe.Handlers.Common
{
    public class HandlersSample1 : Handler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, Dictionary<string, object> Context)
        {
           
            return Task.FromResult(new TestResponse1() { text = response.text });
        }
    }

    public class HandlersSample1Error : Handler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, Dictionary<string, object> Context)
        {

            return Task.FromResult(new TestResponse1() { text = response.text , AnyError  = true});
        }
    }

    public class HandlersSample2 : Handler<TestRequest2, TestResponse2>
    {
        public override Task<TestResponse2> Handle(TestRequest2 response, Dictionary<string, object> Context)
        {
            throw new NotImplementedException();
        }
    }

    public class HandlersSampleWithError : PreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, Dictionary<string, object> Context)
        {
            return Task.FromResult(new TestResponse1() { AnyError = true });
        }
    }

    public class HandlersSampleNOError : PreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, Dictionary<string, object> Context)
        {
            Context.Add("TEST", 1);
            return Task.FromResult(new TestResponse1() { AnyError = false });
        }
    }

   
}
