#### ZeroCoffe Simple Mediator Pattern Chanlog

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