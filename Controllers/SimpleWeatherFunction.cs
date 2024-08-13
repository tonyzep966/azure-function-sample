using azure_function_sample.Services;
using azure_function_sample.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace azure_function_sample.Controllers
{
    public class SimpleWeatherFunction
    {
        private readonly ILogger<SimpleWeatherFunction> _logger;
        private readonly IWeatherService _weatherService;

        public SimpleWeatherFunction(ILogger<SimpleWeatherFunction> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [Function("weather")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header, Description = "API key needed to access the API")]
        [OpenApiOperation(operationId: "Run", tags: ["weather"], Summary = "Get weather by address", Description = "This endpoint returns weather information by address.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "address", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "Address", Description = "The address to get weather information.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OpenMeteoApiResponse), Summary = "Successful operation", Description = "The weather information.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(object), Summary = "Bad request", Description = "Address is required.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(object), Summary = "Internal server error", Description = "An error occurred while processing the request.")]
        public async Task<IActionResult> Run([HttpTrigger(Microsoft.Azure.Functions.Worker.AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("-------------------------------------");
            _logger.LogInformation("Weather function processed a request.");
            _logger.LogInformation("-------------------------------------");

            try
            {
                // Read query parameter
                IDictionary<string, string> queryParams = req.GetQueryParameterDictionary();
                if (queryParams.TryGetValue("address", out string? address))
                {
                    _logger.LogInformation($"address: {address}");
                }

                if (string.IsNullOrEmpty(address))
                {
                    return new JsonResult(new { error = "Address is required." }) { StatusCode = StatusCodes.Status400BadRequest };
                }

                var weatherResponse = await _weatherService.GetWeatherByAddress(address);
                return new JsonResult(weatherResponse) { StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return new JsonResult(new { error = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
