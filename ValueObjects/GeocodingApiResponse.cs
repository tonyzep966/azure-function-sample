using System.Text.Json.Serialization;

namespace azure_function_sample.ValueObjects
{
    public class GeocodingApiResponse
    {
        [JsonPropertyName("place_id")]
        public int PlaceId { get; set; }

        [JsonPropertyName("licence")]
        public required string Licence { get; set; }

        [JsonPropertyName("osm_type")]
        public required string OsmType { get; set; }

        [JsonPropertyName("osm_id")]
        public int OsmId { get; set; }

        [JsonPropertyName("boundingbox")]
        public required List<string> Boundingbox { get; set; }

        [JsonPropertyName("lat")]
        public required string Lat { get; set; }

        [JsonPropertyName("lon")]
        public required string Lon { get; set; }

        [JsonPropertyName("display_name")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("class")]
        public required string Class { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("importance")]
        public double Importance { get; set; }
    }
}
