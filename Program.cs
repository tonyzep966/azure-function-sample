using azure_function_sample.Mappers;
using azure_function_sample.Middlewares;
using azure_function_sample.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    // Use ConfigureFunctionsWebApplication instead of ConfigureFunctionsWorkerDefaults to work with ASP.NET Core integration
    // refer to https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide?tabs=windows#aspnet-core-integration
    // and https://dotnet-worker-rules.azurewebsites.net/rules?ruleid=AZFW0014
    .ConfigureFunctionsWebApplication(
        //functionWorkerApplicationBuilder => functionWorkerApplicationBuilder.UseNewtonsoftJson()
        functionWorkerApplicationBuilder => functionWorkerApplicationBuilder.UseMiddleware<ExceptionLoggingMiddleware>()
    )
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddHttpClient<IDataFetchService>((serviceProvider, client) =>
        {
            // TODO
        }).ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
        {
            //This is infinite by default
            //PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
            MaxConnectionsPerServer = 10
        });
    })
    .Build();

host.Run();
