## ZeroCoffe Simple Mediator Pattern

#### How to use version 1.1.0 [![ZeroCoffe](https://circleci.com/gh/circleci/circleci-docs.svg?style=shield)](https://circleci.com/gh/G3ecko/ZeroCoffe)

You can check a full example at : https://github.com/G3ecko/ZeroCoffe/tree/master/ZeroCoffe.ExampleApi

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
app.RegisterService<ExampleRequest, ValidationExample>();
app.RegisterService<ExampleRequest, HandleExample>();
```

##### Add mediator in controller
```
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

```
##### Add new handler and validation handler
```
public class ValidationExample : PreHandler<ExampleRequest, ExampleResponse>
{

    public override Task<ExampleResponse> Handle(ExampleRequest response, Dictionary<string, object> Context)
    {
        ExampleResponse exampleResponse = new ExampleResponse();
        exampleResponse.AnyError = (response == null || string.IsNullOrEmpty(response.text));
        return Task.FromResult(exampleResponse);
    }
}

public class HandleExample : Handler<ExampleRequest, ExampleResponse>
{

    public override Task<ExampleResponse> Handle(ExampleRequest response, Dictionary<string, object> Context)
    {
        ExampleResponse exampleResponse = new ExampleResponse();
        exampleResponse.textOutput = response.text;
        return Task.FromResult(exampleResponse);
    }
}
```

##### Add new handler response and request
```
public class ExampleResponse : IResponse
{
    public ExampleResponse()
    {

    }

    public ExampleResponse(bool anyError)
    {
        AnyError = anyError;

    }

    public bool AnyError { get; set; }
    public string textOutput { get; set; }
}

public class ExampleRequest: IRequest
{
    public string text { get; set; }
}
```
