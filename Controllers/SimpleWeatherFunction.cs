using azure_function_sample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Text.Json;

namespace azure_function_sample.Controllers
{
    public class SimpleWeatherFunction
    {
        private readonly ILogger<SimpleWeatherFunction> _logger;

        public SimpleWeatherFunction(ILogger<SimpleWeatherFunction> logger)
        {
            _logger = logger;
        }

        [Function("weather")]
        public IActionResult Run([HttpTrigger(Microsoft.Azure.Functions.Worker.AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("-------------------------------------");
            _logger.LogInformation("Weather function processed a request.");
            _logger.LogInformation("-------------------------------------");

            // Read query parameter
            IDictionary<string, string> queryParams = req.GetQueryParameterDictionary();
            if (queryParams.TryGetValue("city", out string? city))
            {
                _logger.LogInformation($"City: {city}");
            }

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
