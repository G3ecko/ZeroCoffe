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
/*
      
        [Fact]
        public async void When_Meditor_Register_One_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
           
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.requestPipeline.Handlers.Count.ShouldBe(1);
        }
  [Fact]
        public async void When_Meditor_Register_One_PreHandler()
        {
            mediator = new Mediator();
            Mock<IPreHandlerRequest> mock = new Mock<IPreHandlerRequest>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();


            mediator.Register<IRequestMock1>(mock.Object);
            mediator.requestPipeline.preHanlders.Count.ShouldBe(1);
        }

        [Fact]
        public async void When_Meditor_Register_Null_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            Should.Throw<ArgumentNullException>(() => mediator.Register<IRequestMock1>(null));
        }

        [Fact]
        public async void When_Meditor_Register_MoreThan_One_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock2>(mock.Object);
            mediator.requestPipeline.Handlers.Count.ShouldBeGreaterThan(1);
        }

        [Fact]
        public async void When_Meditor_Register_MoreThan_One_WithSameType()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock1>(mock.Object);
            mediator.requestPipeline.Handlers.Count.ShouldBeGreaterThan(1);
        }


        [Fact]
        public async void When_Meditor_Handle_One_Request()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> mock1 = new Mock<IRequestMock1>();
            Mock<IResponse> responseMock = new Mock<IResponse>();

            responseMock.Setup(o => o.AnyError).Returns(false);
            mock.Setup(o => o.RequestHandle(mock1.Object, ctxMock)).Returns(Task.FromResult(responseMock.Object));

            Type type = mock1.Object.GetType();
            mock.Setup(o => o.GetRequestType()).Returns(type);

            mediator.Register<IRequestMock1>(mock.Object);
            await mediator.HandleRequest(mock1.Object);

            mock.Verify(o => o.RequestHandle(mock1.Object, ctxMock), Times.Once);

        }


        [Fact]
        public async void When_Meditor_Handle_MoreThanOne_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> reqMock = new Mock<IRequestMock1>();
            Mock<IRequestHandlerMock> mock2 = new Mock<IRequestHandlerMock>();
            Mock<IRequestMock2> reqMock2 = new Mock<IRequestMock2>();
            Mock<IResponse> responseMock = new Mock<IResponse>();

            responseMock.Setup(o => o.AnyError).Returns(false);

            Type type = reqMock.Object.GetType();
            mock.Setup(o => o.GetRequestType()).Returns(type);

            Type type2 = reqMock2.Object.GetType();
            mock2.Setup(o => o.GetRequestType()).Returns(type2);

            mock.Setup(o => o.RequestHandle(reqMock.Object, ctxMock)).Returns(Task.FromResult(responseMock.Object)); ;
            mock2.Setup(o => o.RequestHandle(reqMock2.Object, ctxMock)).Returns(Task.FromResult(responseMock.Object)); ;


            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock2>(mock2.Object);

            await mediator.HandleRequest(reqMock.Object);
            await mediator.HandleRequest(reqMock2.Object);

            mock.Verify(o => o.RequestHandle(reqMock.Object, ctxMock), Times.Once);
            mock2.Verify(o => o.RequestHandle(reqMock2.Object, ctxMock), Times.Once);
        }



        [Fact]
        public async void When_Meditor_Handle_MoreThanOne_WithSameKey_Handler()
        {
            mediator = new Mediator();
            Mock<IRequestHandler> mock = new Mock<IRequestHandler>();
            Mock<IRequestMock1> reqMock = new Mock<IRequestMock1>();
            Mock<IRequestHandlerMock> mock2 = new Mock<IRequestHandlerMock>();
            Mock<IResponse> responseMock = new Mock<IResponse>();

            responseMock.Setup(o => o.AnyError).Returns(false);

            Type type = reqMock.Object.GetType();
            mock.Setup(o => o.GetRequestType()).Returns(type);

            Type type2 = reqMock.Object.GetType();
            mock2.Setup(o => o.GetRequestType()).Returns(type2);

            mock.Setup(o => o.RequestHandle(reqMock.Object, ctxMock)).Returns(Task.FromResult(responseMock.Object)); ;
            mock2.Setup(o => o.RequestHandle(reqMock.Object, ctxMock)).Returns(Task.FromResult(responseMock.Object)); ;


            mediator.Register<IRequestMock1>(mock.Object);
            mediator.Register<IRequestMock1>(mock2.Object);

            await mediator.HandleRequest(reqMock.Object);
     

            mock.Verify(o => o.RequestHandle(reqMock.Object, ctxMock), Times.Once);
            mock2.Verify(o => o.RequestHandle(reqMock.Object, ctxMock), Times.Once);
        }


       */
    }
}

