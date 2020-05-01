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
        public async void When_HandlersServiceProvider_Register_One_Handler()
        {
            Mock<IHandlersServiceProvider> providerMock = new Mock<IHandlersServiceProvider>();
            RequestPipeline provider = new RequestPipeline(providerMock.Object);

           // provider.ExecPipeline()
           //
          
            
        }
    }
}