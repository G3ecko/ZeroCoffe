## ZeroCoffe Simple Mediator Pattern

#### How to use version 1.0.0 [![ZeroCoffe](https://circleci.com/gh/circleci/circleci-docs.svg?style=shield)](https://circleci.com/gh/G3ecko/ZeroCoffe)


#####  Add meditor service
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddSingleton<IMediator, Mediator>();
}
```
##### Add Handler Service
```
app.RegisterService<WeatherHandlerRequest, WeatherHandler>();
```

##### Add mediator in controller
```
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

```
##### Add new handler
```
public class WeatherHandler : BaseHandler<WeatherHandlerRequest, WeatherHandlerResponse>
    {
        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public override Task<WeatherHandlerResponse> Handle(WeatherHandlerRequest response, Dictionary<string, object> Context)
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
```

##### Add new handler response and request
```
    public class WeatherHandlerResponse : IResponse
    {
        public WeatherHandlerResponse()
        {

        }

        public WeatherHandlerResponse(bool anyError, IEnumerable<WeatherForecast> weatherForecasts)
        {
            AnyError = anyError;
            this.weatherForecasts = weatherForecasts;
        }

        public bool AnyError { get; set; }
        public IEnumerable<WeatherForecast> weatherForecasts { get; set; }
    }

    public class WeatherHandlerRequest : IRequest
    {
    }
```