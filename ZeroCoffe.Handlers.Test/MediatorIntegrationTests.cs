using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ZeroCoffe.Handlers.Common;
using ZeroCoffe.Handlers.Interface;
using ZeroCoffe.Handlers.Test.Common;


namespace ZeroCoffe.Handlers.Test
{
  
    public  class MediatorIntegrationTests
    {
        Mediator mediator;
        Dictionary<string, object> ctxMock = new Dictionary<string, object>();

        [Fact]
        public async void When_Meditor_HandleRequest_One()
        {
            mediator = new Mediator();
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSample1());
            var results = await mediator.HandleRequest(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.ShouldNotBeNull();
            ouput.text.ShouldBe(testeRequest.text);

        }


        [Fact]
        public async void When_Meditor_HandleRequest_WithError()
        {
            mediator = new Mediator();
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSample1Error());
            var results = await mediator.HandleRequest(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.ShouldNotBeNull();
            ouput.AnyError.ShouldBeTrue();

        }

        [Fact]
        public async void When_Meditor_HandleRequest_NoError()
        {
          
            mediator = new Mediator();
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSample1());
            var results = await mediator.HandleRequest(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.ShouldNotBeNull();
            ouput.AnyError.ShouldBeFalse();
            ouput.text.ShouldBe(testeRequest.text);

        }

        [Fact]
        public async void When_Meditor_WithPreHandler_WithError()
        {
            mediator = new Mediator();
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSampleWithError());
            mediator.Register<TestRequest1>(new HandlersSample1());
            var results = await mediator.HandleRequest(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.AnyError.ShouldBeTrue();

        }

        [Fact]
        public async void When_Meditor_WithPreHandler_WithNoError()
        {
            mediator = new Mediator();
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSampleNOError());
            mediator.Register<TestRequest1>(new HandlersSample1());
            var results = await mediator.HandleRequest(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.AnyError.ShouldBeFalse();
            ouput.text.ShouldBe(testeRequest.text);

        }
    }
}
