using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroCoffe.Handlers;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.ExampleApi.Handlers
{
    public class WeatherHandler : Handler<WeatherHandlerRequest, WeatherHandlerResponse>
    {
        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public override Task<WeatherHandlerResponse> Handle(WeatherHandlerRequest response, IDictionary<string, object> Context)
        {

           var rng = new Random();
           var res = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Task.FromResult(new WeatherHandlerResponse(false, res));
        }
    }


    public class WeatherHandlerValidation : PreHandler<WeatherHandlerRequest, WeatherHandlerResponse>
    {
   
        public override Task<WeatherHandlerResponse> Handle(WeatherHandlerRequest response, IDictionary<string, object> Context)
        {
            WeatherHandlerResponse weatherHandlerResponse = new WeatherHandlerResponse();
            weatherHandlerResponse.AnyError = (string.IsNullOrEmpty(response.text));
            return Task.FromResult(weatherHandlerResponse); 
        }
    }

    public class WeatherHandlerResponse : BaseResponse
    {
        public WeatherHandlerResponse()
        {

        }

        public WeatherHandlerResponse(bool anyError, IEnumerable<WeatherForecast> weatherForecasts)
        {
            AnyError = anyError;
            this.weatherForecasts = weatherForecasts;
        }

      
        public IEnumerable<WeatherForecast> weatherForecasts { get; set; }
    
    }

    public class WeatherHandlerRequest : IRequest
    {
        public string text { get; set; }
    }
}
