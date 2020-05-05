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

    public class RequestHandlerPipelineParallelTests
    {
        [Fact]
        public async void When_RequestPipelineParallel_ExecPipeLine_Parallel_With_One_Handler()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1() });
            RequestPipelineParallel provider = new RequestPipelineParallel(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.GetResponse<TestResponse1>().ShouldNotBeNull();
            res.ResponseHasErros().ShouldBeFalse();
            ((TestResponse1)res.FirstOrDefault()).text.ShouldBe(testRequest1.text);
            res.Count.ShouldBe(1);

        }

        [Fact]
        public async void When_RequestPipelineParallel_ExecPipeLine_Parallel_With_MoreThanOne_Handler()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            TestRequest2 testRequest2 = new TestRequest2();
            testRequest2.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1() });
            providerMock.Setup(o => o.GetHandlers(testRequest2)).Returns(new List<IBaseHandler> { new HandlersSample2() });
            RequestPipelineParallel provider = new RequestPipelineParallel(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);
            var res2 = await provider.ExecPipeline(testRequest2);

            res.GetResponse<TestResponse1>().ShouldNotBeNull();
            res.ResponseHasErros().ShouldBeFalse();
            ((TestResponse1)res.FirstOrDefault()).text.ShouldBe(testRequest1.text);
            res.Count.ShouldBe(1);

            res2.GetResponse<TestResponse2>().ShouldNotBeNull();
            res2.ResponseHasErros().ShouldBeFalse();
            ((TestResponse2)res2.FirstOrDefault()).text.ShouldBe(testRequest2.text);
            res2.Count.ShouldBe(1);

        }


        [Fact]
        public async void When_RequestPipelineParallel_ExecPipeLine_Parallel_With_MoreThanOne_Handler_SameKey()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSampleNOError() });

            RequestPipelineParallel provider = new RequestPipelineParallel(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            var response = res.GetResponse<TestResponse1>();
            response.ShouldNotBeNull();
            res.ResponseHasErros().ShouldBeFalse();
            response.text.ShouldBe(testRequest1.text);
            res.Count.ShouldBe(2);

        }

        [Fact]
        public async void When_RequestPipelineParallel_ExecPipeLine_Parallel_With_MoreThanOne_Handler_SameKey_Response()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSample_1() });

            RequestPipelineParallel provider = new RequestPipelineParallel(providerMock.Object);

            var res = await provider.ExecPipeline(testRequest1);

            res.ShouldNotBeNull();
            res.Count.ShouldBe(2);
            var responses = res.GetResponses<TestResponse1>();

            foreach (var response in responses)
            {
                response.ShouldNotBeNull();
                response.text.ShouldBe(testRequest1.text);
            }
        }


        [Fact]
        public async void When_RequestPipelineParallel_ExecPipeLine_Parallel_With_MoreThanOne_Handler_Error()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSample1Error() });

            RequestPipelineParallel provider = new RequestPipelineParallel(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.GetResponse<TestResponse1>().ShouldNotBeNull();
            res.ResponseHasErros().ShouldBeTrue();
            res.Count.ShouldBe(2);

        }

        [Fact]
        public async void When_RequestPipelineParallel_ExecPipeLine_Parallel_With_MoreThanOne_PreHandler_Error()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSampleWithError() });

            RequestPipelineParallel provider = new RequestPipelineParallel(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);

            res.FirstOrDefault().ShouldNotBeNull();
            res.Count.ShouldBe(1);
            res.Any(o => o.AnyError).ShouldBeTrue();


        }


        [Fact]
        public async void When_RequestPipelineParallel_ExecPipeLine_Parallel_With_MoreThanOne_Execpetion()
        {

            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            TestRequest1 testRequest1 = new TestRequest1();
            testRequest1.text = "1";
            providerMock.Setup(o => o.GetHandlers(testRequest1)).Returns(new List<IBaseHandler> { new HandlersSample1(), new HandlersSampleWithErrorExecption() });

            RequestPipelineParallel provider = new RequestPipelineParallel(providerMock.Object);


            var res = await provider.ExecPipeline(testRequest1);


            res.ResponseHasErros().ShouldBeTrue();
            res.GetErrorResponse<DefaultResponseError>().ShouldNotBeNull();
        }

     
    }
}