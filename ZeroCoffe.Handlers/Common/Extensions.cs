using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers.Common
{
    public static class Extensions
    {
        public static void RegisterService<IService, TImplementation>(this IApplicationBuilder app) where TImplementation : IRequestHandler
        {
            var mediator = app.ApplicationServices.GetService<IMediator>();
            if (mediator != null)
            {

                mediator.Register<IService>(Activator.CreateInstance<TImplementation>());
            }

        }

        public static TResponse GetResponse<TResponse>(this List<IResponse> responses) where TResponse : IResponse
        {
            if (responses != null && responses.Count > 0)
            {
                return (TResponse)responses.FirstOrDefault(o => o.GetType() == typeof(TResponse));
            }
            return (TResponse)default;
        }

        public static bool ResponseHasErros(this List<IResponse> responses) => responses != null && responses.Count > 0 && responses.Any(o => o.AnyError);
        

        public static List<TResponse> GetResponses<TResponse>(this List<IResponse> responses) where TResponse : IResponse
        {
            if (responses != null && responses.Count > 0)
            {
                return (List<TResponse>)responses.Where(o => o.GetType() == typeof(TResponse));
            }
            return (List<TResponse>)default;
        }
    }
}
