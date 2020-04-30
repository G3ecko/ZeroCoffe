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
    [Route("api/[controller]/{action}/")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator mediator;

        public WeatherForecastController(IMediator mediator, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await mediator.HandleRequest(new WeatherHandlerRequest());
            if (res.Any(o => o.AnyError))
            {
                return BadRequest();
            }
            else
            {
                return Ok(res.GetResponse<WeatherHandlerResponse>()?.weatherForecasts);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetWithValidator(string text)
        {
            var request = new ExampleRequest();
            request.text = text;

            var res = await mediator.HandleRequest(request);
            if (res.Any(o => o.AnyError))
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res.GetResponse<ExampleResponse>());
            }

        }
    }
}
