using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCoffe.Handlers.Common
{
    public class HandlersSample1 : Handler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {
           
            return Task.FromResult(new TestResponse1() { text = response.text });
        }
    }

    public class HandlersSample_1 : Handler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {

            return Task.FromResult(new TestResponse1() { text = response.text });
        }
    }

    public class HandlersSample1Error : Handler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {

            return Task.FromResult(new TestResponse1() { text = response.text , AnyError  = true});
        }
    }

    public class HandlersSample2 : Handler<TestRequest2, TestResponse2>
    {
        public override Task<TestResponse2> Handle(TestRequest2 response, IDictionary<string, object> Context)
        {
            return Task.FromResult(new TestResponse2() { text = response.text });
        }
    }

    public class HandlersSampleWithError : PreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {
            return Task.FromResult(new TestResponse1() { AnyError = true });
        }
    }

    public class HandlersSampleWithErrorExecption : PreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {

            throw new NotImplementedException();
        }
    }

    public class HandlersSampleNOError : PreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {
            Context.Add("TEST", 1);
            return Task.FromResult(new TestResponse1() { AnyError = false });
        }
    }


    public class HandlersSamplePreContext : PreHandler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {
            Context.Add("TEST", "Init");
            return Task.FromResult(new TestResponse1() { AnyError = false,text= Context["TEST"].ToString()});
        }
    }

    public class HandlersSampleContext1 : Handler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {
            string text = Context["TEST"].ToString();
            Context["TEST"] = "HandlersSampleContext1";
            return Task.FromResult(new TestResponse1() { AnyError = false , text= text });
        }
    }

    public class HandlersSampleContext2 : Handler<TestRequest1, TestResponse1>
    {
        public override Task<TestResponse1> Handle(TestRequest1 response, IDictionary<string, object> Context)
        {
            string text = Context["TEST"].ToString();
            Context["TEST"] = "HandlersSampleContext2";
            return Task.FromResult(new TestResponse1() { AnyError = false, text = text });
        }
    }

}
