using System.Text.Json.Serialization;

namespace azure_function_sample.Models
{
    public class PostRequestBodySampleModel

    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }
}
