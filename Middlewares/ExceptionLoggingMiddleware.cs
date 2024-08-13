using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using System.Net;

namespace azure_function_sample.Middlewares
{
    public class ExceptionLoggingMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                // Code before function execution here
                await next(context);
                // Code after function execution here
            }
            catch (Exception ex)
            {
                var log = context.GetLogger<ExceptionLoggingMiddleware>();
                log.LogError(ex.Message);
                
                var request = await context.GetHttpRequestDataAsync();
                var response = request!.CreateResponse();
                await response.WriteAsJsonAsync(new { error = ex.Message }, HttpStatusCode.InternalServerError);
                context.GetInvocationResult().Value = response;
            }
        }
    }
}
