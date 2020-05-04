namespace ZeroCoffe.Handlers.Test
{
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

    public class RequestHandlerPipelineTests
    {
        [Fact]
        public async void When_RequestPipeline_ExecPipeLine_With_One_Handler()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1() });
            RequestPipeline provider = new RequestPipeline(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.FirstOrDefault().ShouldNotBeNull();
            res.FirstOrDefault().ShouldBeOfType(typeof(TestResponse1));
            res.FirstOrDefault().AnyError.ShouldBeFalse();
            ((TestResponse1)res.FirstOrDefault()).text.ShouldBe(testRequest1.text);
            res.Count.ShouldBe(1);

        }

        [Fact]
        public async void When_RequestPipeline_ExecPipeLine_With_MoreThanOne_Handler()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            TestRequest2 testRequest2 = new TestRequest2();
            testRequest2.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1() });
            providerMock.Setup(o => o.GetHandlers(testRequest2)).Returns(new List<IBaseHandler> { new HandlersSample2() });
            RequestPipeline provider = new RequestPipeline(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);
            var res2 = await provider.ExecPipeline(testRequest2);

            res.FirstOrDefault().ShouldNotBeNull();
            res.FirstOrDefault().ShouldBeOfType(typeof(TestResponse1));
            res.FirstOrDefault().AnyError.ShouldBeFalse();
            ((TestResponse1)res.FirstOrDefault()).text.ShouldBe(testRequest1.text);
            res.Count.ShouldBe(1);

            res2.FirstOrDefault().ShouldNotBeNull();
            res2.FirstOrDefault().ShouldBeOfType(typeof(TestResponse2));
            res2.FirstOrDefault().AnyError.ShouldBeFalse();
            ((TestResponse2)res2.FirstOrDefault()).text.ShouldBe(testRequest2.text);
            res2.Count.ShouldBe(1);

        }


        [Fact]
        public async void When_RequestPipeline_ExecPipeLine_With_MoreThanOne_Handler_SameKey()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSampleNOError() });

            RequestPipeline provider = new RequestPipeline(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.FirstOrDefault().ShouldNotBeNull();
            res.FirstOrDefault().ShouldBeOfType(typeof(TestResponse1));
            res.FirstOrDefault().AnyError.ShouldBeFalse();
            ((TestResponse1)res.FirstOrDefault()).text.ShouldBe(testRequest1.text);
            res.Count.ShouldBe(1);

        }

        [Fact]
        public async void When_RequestPipeline_ExecPipeLine_With_MoreThanOne_Handler_SameKey_Response()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSample_1() });

            RequestPipeline provider = new RequestPipeline(providerMock.Object);

            var res = await provider.ExecPipeline(testRequest1);
            res.ShouldNotBeNull();
            res.Count.ShouldBe(2);

            foreach (var r in res)
            {
                var result = r as TestResponse1;
                result.ShouldNotBeNull();
                result.text.ShouldBe(testRequest1.text);
            }
        }


        [Fact]
        public async void When_RequestPipeline_ExecPipeLine_With_MoreThanOne_Handler_Error()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSample1Error() });

            RequestPipeline provider = new RequestPipeline(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.FirstOrDefault().ShouldNotBeNull();
            res.Any(o => o.AnyError).ShouldBeTrue();
            res.Count.ShouldBe(2);

        }

        [Fact]
        public async void When_RequestPipeline_ExecPipeLine_With_MoreThanOne_PreHandler_Error()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSampleWithError() });

            RequestPipeline provider = new RequestPipeline(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.FirstOrDefault().ShouldNotBeNull();
            res.Count.ShouldBe(1);
            res.Any(o => o.AnyError).ShouldBeTrue();


        }


        [Fact]
        public async void When_RequestPipeline_ExecPipeLine_With_MoreThanOne_Execpetion()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSampleWithErrorExecption() });

            RequestPipeline provider = new RequestPipeline(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.FirstOrDefault().ShouldNotBeNull();
            res.Count.ShouldBe(1);
            res.Any(o => o.AnyError).ShouldBeTrue();
            res.FirstOrDefault().ShouldBeOfType(typeof(DefaultResponseError));


        }
    }
}