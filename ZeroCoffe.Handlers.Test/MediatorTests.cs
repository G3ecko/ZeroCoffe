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
    public class MediatorTests
    {
        Mediator mediator;
        Dictionary<string, object> ctxMock = new Dictionary<string, object>();

        [Fact]
        public async void When_Meditor_Register_One_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.handlers.Count.ShouldBe(1);
        }

        [Fact]
        public async void When_Meditor_Register_MoreThan_One_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            Mock<IRequestMock2> mock2 = new Mock<IRequestMock2>();

            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock2>(mock.Object);
            mediator.handlers.Count.ShouldBeGreaterThan(1);
        }

        [Fact]
        public async void When_Meditor_Register_MoreThan_One_WithSameKey_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.handlers.Values.FirstOrDefault()?.Count.ShouldBeGreaterThan(1);
        }


        [Fact]
        public async void When_Meditor_Handler_No_Handler_Registered()
        {
            mediator = new Mediator();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            Should.Throw<KeyNotFoundException>(() => mediator.GetRegisteredRequestHandlers<IRequestMock1>());

        }

        [Fact]
        public async void When_Meditor_Get_One_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            mediator.Register<IRequestMock1>(mock.Object);

            var res = mediator.GetRegisteredRequestHandlers<IRequestMock1>();

            res.ShouldNotBeNull();
            res.Count.ShouldBe(1);
        }

        [Fact]
        public async void When_Meditor_Handle_One_Request()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            mock.Setup(o => o.RequestHandle(mock1.Object, ctxMock));
            mediator.Register<IRequestMock1>(mock.Object);

            var res = mediator.GetRegisteredRequestHandlers<IRequestMock1>();
            await res.FirstOrDefault().RequestHandle(mock1.Object, ctxMock);

            mock.Verify(o => o.RequestHandle(mock1.Object, ctxMock), Times.Once);

        }

        [Fact]
        public async void When_Meditor_Get_MoreThanOne_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> reqMock = new Mock<IRequestMock1>();
            Mock<IRequestHandlerMock> mock2 = new Mock<IRequestHandlerMock>();
            Mock<IRequestMock2> reqMock2 = new Mock<IRequestMock2>();
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock2>(mock2.Object);

            var res = mediator.GetRegisteredRequestHandlers<IRequestMock1>();
            var res2 = mediator.GetRegisteredRequestHandlers<IRequestMock2>();

            res.ShouldNotBeNull();
            res.Count.ShouldBe(1);

            res2.ShouldNotBeNull();
            res2.Count.ShouldBe(1);
        }

        [Fact]
        public async void When_Meditor_Handle_MoreThanOne_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> reqMock = new Mock<IRequestMock1>();
            Mock<IRequestHandlerMock> mock2 = new Mock<IRequestHandlerMock>();
            Mock<IRequestMock2> reqMock2 = new Mock<IRequestMock2>();
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock2>(mock2.Object);


            mock.Setup(o => o.RequestHandle(reqMock.Object, new Dictionary<string, object>()));
            mock2.Setup(o => o.RequestHandle(reqMock2.Object, new Dictionary<string, object>()));

            var res = mediator.GetRegisteredRequestHandlers<IRequestMock1>();
            await res.FirstOrDefault().RequestHandle(reqMock.Object, new Dictionary<string, object>());
            var res2 = mediator.GetRegisteredRequestHandlers<IRequestMock2>();
            await res2.FirstOrDefault().RequestHandle(reqMock2.Object, new Dictionary<string, object>());

            mock.Verify(o => o.RequestHandle(reqMock.Object, ctxMock), Times.Once);
            mock2.Verify(o => o.RequestHandle(reqMock2.Object, ctxMock), Times.Once);
        }

        [Fact]
        public async void When_Meditor_Get_MoreThanOne_WithSameKey_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> reqMock = new Mock<IRequestMock1>();
            Mock<IRequestHandlerMock> mock2 = new Mock<IRequestHandlerMock>();

            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock1>(mock2.Object);

            var res = mediator.GetRegisteredRequestHandlers<IRequestMock1>();

            res.ShouldNotBeNull();
            res.Count.ShouldBe(2);

        }

        [Fact]
        public async void When_Meditor_Handle_MoreThanOne_WithSameKey_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> reqMock = new Mock<IRequestMock1>();
            Mock<IRequestHandlerMock> mock2 = new Mock<IRequestHandlerMock>();

            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock1>(mock2.Object);

            mock.Setup(o => o.RequestHandle(reqMock.Object, ctxMock));
            mock2.Setup(o => o.RequestHandle(reqMock.Object, ctxMock));

            var res = mediator.GetRegisteredRequestHandlers<IRequestMock1>().ToList();
            res.ForEach(o => o.RequestHandle(reqMock.Object, ctxMock));


            mock.Verify(o => o.RequestHandle(reqMock.Object, ctxMock), Times.Once);
            mock2.Verify(o => o.RequestHandle(reqMock.Object, ctxMock), Times.Once);
        }


        [Fact]
        public async void When_Meditor_IntegrationTest_HandleRequest_One()
        {
            mediator = new Mediator();
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSample1());
            var results = await mediator.HandleRequest<TestRequest1>(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.ShouldNotBeNull();
            ouput.text.ShouldBe(testeRequest.text);

        }


        [Fact]
        public async void When_Meditor_IntegrationTest_HandleRequest_WithPipeline_WithError()
        {
            var pipeLine = new BaseRequestPipeline();
            pipeLine.AddNewHandler(new HandlersSampleWithError(), HandlerType.PRE_HANDLE);
            mediator = new Mediator(pipeLine);
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSample1());
            var results = await mediator.HandleRequest<TestRequest1>(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.ShouldNotBeNull();
            ouput.AnyError.ShouldBeTrue();

        }

        [Fact]
        public async void When_Meditor_IntegrationTest_HandleRequest_WithPipeline_NoError()
        {
            var pipeLine = new BaseRequestPipeline();
            pipeLine.AddNewHandler(new HandlersSampleNOError(), HandlerType.PRE_HANDLE);
            mediator = new Mediator(pipeLine);
            TestRequest1 testeRequest = new TestRequest1();
            testeRequest.text = "babababababa";

            mediator.Register<TestRequest1>(new HandlersSample1());
            var results = await mediator.HandleRequest<TestRequest1>(testeRequest);

            var ouput = results.FirstOrDefault() as TestResponse1;

            ouput.ShouldNotBeNull();
            ouput.AnyError.ShouldBeFalse();
            ouput.text.ShouldBe(testeRequest.text);

        }
    }
}

