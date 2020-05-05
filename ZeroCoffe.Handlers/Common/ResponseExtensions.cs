using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers.Common
{
    public static class ResponseExtensions
    {
        public static TResponse GetResponse<TResponse>(this IList<IResponse> responses) where TResponse : IResponse
        {
            if (responses != null && responses.Count > 0)
            {
                return (TResponse)responses.FirstOrDefault(o => o.GetType() == typeof(TResponse) && !o.AnyError && o.HandledBy == typeof(IRequestHandler).Name);
            }
            return (TResponse)default;
        }

        public static TResponse GetPreResponse<TResponse>(this IList<IResponse> responses) where TResponse : IResponse
        {
            if (responses != null && responses.Count > 0)
            {
                return (TResponse)responses.FirstOrDefault(o => o.GetType() == typeof(TResponse) && !o.AnyError && o.HandledBy == typeof(IPreHandlerRequest).Name);
            }
            return (TResponse)default;
        }

        public static bool ResponseHasErros(this IList<IResponse> responses) => responses != null && responses.Count > 0 && responses.Any(o => o.AnyError);

        public static TResponse GetErrorResponse<TResponse>(this IList<IResponse> responses) where TResponse : IResponse
        {
            if (responses != null && responses.Count > 0)
            {
                return (TResponse)responses.FirstOrDefault(o => o.GetType() == typeof(TResponse) && o.AnyError);
            }
            return (TResponse)default;
        }

        public static List<TResponse> GetResponses<TResponse>(this IList<IResponse> responses) where TResponse : IResponse
        {
            List<TResponse> res = new List<TResponse>();
            if (responses != null && responses.Count > 0)
            {
                var responsesAux = responses.Where(o => o.GetType() == typeof(TResponse) && !o.AnyError && o.HandledBy == typeof(IRequestHandler).Name);
                foreach (var resp in responsesAux)
                {
                    res.Add((TResponse)resp);
                }
                return res;
            }
            return (List<TResponse>)default;
        }

        public static List<TResponse> GetPreResponses<TResponse>(this IList<IResponse> responses) where TResponse : IResponse
        {
            List<TResponse> res = new List<TResponse>();
            if (responses != null && responses.Count > 0)
            {
                var responsesAux = responses.Where(o => o.GetType() == typeof(TResponse) && !o.AnyError && o.HandledBy == typeof(IPreHandlerRequest).Name);
                foreach (var resp in responsesAux)
                {
                    res.Add((TResponse)resp);
                }
                return res;
            }
            return (List<TResponse>)default;
        }
    }
}
