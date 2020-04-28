using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZeroCoffe.ExampleApi.Handlers;
using ZeroCoffe.Handlers.Interface;
using ZeroCoffe.Handlers.Common;

namespace ZeroCoffe.ExampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator mediator;

        public WeatherForecastController(IMediator mediator ,ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var res =  mediator.HandleRequest<WeatherHandlerRequest>();
            return (await res).GetResponse<WeatherHandlerResponse>()?.weatherForecasts;
        }
    }
}
