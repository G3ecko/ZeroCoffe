## ZeroCoffe Simple Mediator Pattern

#### How to use version 2.0.0 Preview [![ZeroCoffe](https://circleci.com/gh/circleci/circleci-docs.svg?style=shield)](https://circleci.com/gh/G3ecko/ZeroCoffe)

You can check a full example at : https://github.com/G3ecko/ZeroCoffe/tree/master/ZeroCoffe.ExampleApi

#### Version 2.0.0 - Preview001
- [x] Change request pipline to improve performance
- [x] Parallel Pipeline
- [x] Create more response extension to help parse result from handlers
- [x] Create exentsions to add mediator services 
- [x] BaseResponse has add to better output

##### Add sequencial or parallel pipeline
```
Add sequencial pipeline
services.UseZeroCoffeMediator();

OR

Add parallel pipeline
services.UseZeroCoffeMediatorParallel();

```
   
##### Response Extensions 
```
TResponse GetResponse<TResponse>(this IList<IResponse> responses)
TResponse GetPreResponse<TResponse>(this IList<IResponse> responses)
bool ResponseHasErros(this IList<IResponse> responses)
TResponse GetErrorResponse<TResponse>(this IList<IResponse> responses)
List<TResponse> GetResponses<TResponse>(this IList<IResponse> responses)
List<TResponse> GetPreResponses<TResponse>(this IList<IResponse> responses)

```

#####  Add meditor service
```

#### How to use version 1.2.0

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddSingleton<IMediator, Mediator>();
    services.AddSingleton<IRequestPipeline, RequestPipeline>();
    services.AddSingleton<IHandlersServiceProvider, HandlersServiceProvider>();
}
```
##### Add Handler Service
```
app.RegisterService<WeatherHandlerRequest, WeatherHandler>();
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
