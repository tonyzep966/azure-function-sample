using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace azure_function_sample.Configs
{
    // Instead of implementing IOpenApiConfigurationOptions, you can inherit from DefaultOpenApiConfigurationOptions and override the properties you want to change.
    // You can also inject the OpenApiConfigurationOptions instance during startup, please refer to https://github.com/Azure/azure-functions-openapi-extension/blob/main/docs/openapi-out-of-proc.md#injecting-openapiconfigurationoptions-during-startup
    public class CustomOpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
    }
}
