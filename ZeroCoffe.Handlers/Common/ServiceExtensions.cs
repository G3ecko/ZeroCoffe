using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers.Common
{
    public static class ServiceExtensions
    {
        public static void UseZeroCoffeMediator(this IServiceCollection services)
        {
            services.AddSingleton<IMediator, Mediator>();
            services.AddSingleton<IRequestPipeline, RequestPipeline>();
            services.AddSingleton<IHandlersServiceProvider, HandlersServiceProvider>();
        }

        public static void UseZeroCoffeMediatorParallel(this IServiceCollection services)
        {
            services.AddSingleton<IMediator, Mediator>();
            services.AddSingleton<IRequestPipeline, RequestPipelineParallel>();
            services.AddSingleton<IHandlersServiceProvider, HandlersServiceProvider>();
        }


        public static void RegisterService<IServiceRequest, TImplementation>(this IApplicationBuilder app) where TImplementation : IBaseHandler where IServiceRequest : IRequest
        {
            var handlersServiceProvider = app.ApplicationServices.GetService<IHandlersServiceProvider>();
            if (handlersServiceProvider != null)
            {
                handlersServiceProvider.Register<IServiceRequest>(Activator.CreateInstance<TImplementation>());
            }
        }
    }
}
