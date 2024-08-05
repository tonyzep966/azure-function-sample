using azure_function_sample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Text.Json;

namespace azure_function_sample.Controllers
{
    public class ExampleFunction
    {
        private readonly ILogger<ExampleFunction> _logger;

        public ExampleFunction(ILogger<ExampleFunction> logger)
        {
            _logger = logger;
        }

        [Function("ExampleFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        // This sample shows how to bind a route parameter to a function parameter
        [Function("GetById")]
        public IActionResult Func2([HttpTrigger(Microsoft.Azure.Functions.Worker.AuthorizationLevel.Function, "get", Route = "getById/{id}")] HttpRequest req, string id)
        {
            _logger.LogInformation("-------------------------------------");
            _logger.LogInformation("Func2 function processed a request.");
            _logger.LogInformation("-------------------------------------");

            return new ContentResult
            {
                Content = $"Hello, {id}",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = StatusCodes.Status200OK
            };
        }

        // This sample shows how to get query parameters
        [Function("GetByName")]
        public IActionResult Get([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("-------------------------------------");
            _logger.LogInformation("Get function processed a request.");
            _logger.LogInformation("-------------------------------------");

            var query = req.Query;
            // Get query parameter "name"
            var name = query["name"];
            // or
            query.TryGetValue("name", out var name2);

            _logger.LogInformation($"Name: {name}");
            _logger.LogInformation($"Name2: {name2}");

            return new ContentResult
            {
                Content = $"Hello, {name}",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = StatusCodes.Status200OK
            };
        }

        // This sample shows how to bind a request body
        [Function("PostExample")]
        public async Task<IActionResult> Post([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("-------------------------------------");
            _logger.LogInformation("Post function processed a request.");
            _logger.LogInformation("-------------------------------------");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var postRequestBody = JsonSerializer.Deserialize<PostRequestBodySampleModel>(requestBody);
            _logger.LogInformation($"Post: {postRequestBody?.Name}");

            return new JsonResult(postRequestBody);
        }
    }
}
