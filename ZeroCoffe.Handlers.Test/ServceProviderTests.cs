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
    public class ServceProviderTests
    {
      

        [Fact]
        public async void When_HandlersServiceProvider_Register_One_Handler()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            provider.Register<TestRequest1>(new HandlersSample1());

            var Handlers = provider.GetHandlers(new TestRequest1());
            Handlers.Count.ShouldBe(1);
            
        }

         [Fact]
        public async void When_HandlersServiceProvider_Register_MoreThanOne_Handler()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            provider.Register<TestRequest1>(new HandlersSample1());
            provider.Register<TestRequest2>(new HandlersSample2());

            var Handlers1 = provider.GetHandlers(new TestRequest1());
            var Handlers2 = provider.GetHandlers(new TestRequest2());
            Handlers1.Count.ShouldBe(1);
            Handlers2.Count.ShouldBe(1);
            
        }

         [Fact]
        public async void When_HandlersServiceProvider_Register_MoreThanOne_Handler_SameRquest()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            provider.Register<TestRequest1>(new HandlersSample1());
            provider.Register<TestRequest1>(new HandlersSample1Error());

            var Handlers1 = provider.GetHandlers(new TestRequest1());
            Handlers1.Count.ShouldBe(2);
          
        }

         [Fact]
        public async void When_HandlersServiceProvider_Register_Request_Mismatch_Handler()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            Should.Throw<ArgumentException>(() =>  provider.Register<TestRequest1>(new HandlersSample2()));
       
        }


        [Fact]
        public async void When_HandlersServiceProvider_Register_NullThanlder()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            Should.Throw<ArgumentNullException>(() => provider.Register<TestRequest1>(null));
        }

        [Fact]
        public async void When_HandlersServiceProvider_Get_Request_Wihoutn_Hanlder()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            Should.Throw<KeyNotFoundException>(() => provider.GetHandlers(new TestRequest1()));
        }

        [Fact]
        public async void When_HandlersServiceProvider_Get_NullThanlder()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            Should.Throw<ArgumentNullException>(() => provider.GetHandlers(null));
        }

        [Fact]
        public async void When_HandlersServiceProvider_Get_Handler()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            provider.Register<TestRequest1>(new HandlersSample1());

            var handler = provider.GetHandlers(new TestRequest1());

            handler.ShouldNotBeNull();
            handler.Count.ShouldBe(1);
            handler.FirstOrDefault().ShouldBeOfType(typeof(HandlersSample1));
            
        }



        [Fact]
        public async void When_HandlersServiceProvider_Get_MoreThan_One_Handler()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            provider.Register<TestRequest1>(new HandlersSample1());
            provider.Register<TestRequest2>(new HandlersSample2());

            var handler = provider.GetHandlers(new TestRequest1());
            var handler2 = provider.GetHandlers(new TestRequest2());

            handler.ShouldNotBeNull();
            handler.Count.ShouldBe(1);
            handler.FirstOrDefault().ShouldBeOfType(typeof(HandlersSample1));

            handler2.ShouldNotBeNull();
            handler2.Count.ShouldBe(1);
            handler2.FirstOrDefault().ShouldBeOfType(typeof(HandlersSample2));

        }

        [Fact]
        public async void When_HandlersServiceProvider_Get_MoreThan_One_Handler_SameKey()
        {
            HandlersServiceProvider provider = new HandlersServiceProvider();
            provider.Register<TestRequest1>(new HandlersSample1());
            provider.Register<TestRequest1>(new HandlersSampleNOError());

            var handler = provider.GetHandlers(new TestRequest1());
    

            handler.ShouldNotBeNull();
            handler.Count.ShouldBe(2);
            handler.FirstOrDefault(o => o.GetType() == (typeof(HandlersSample1))).ShouldNotBeNull();
            handler.FirstOrDefault(o => o.GetType() == (typeof(HandlersSampleNOError))).ShouldNotBeNull();
        }
    }
}