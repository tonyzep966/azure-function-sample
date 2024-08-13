using azure_function_sample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

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
