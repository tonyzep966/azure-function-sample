using System.Text.Json.Serialization;

namespace azure_function_sample.ValueObjects
{
    public class OpenMeteoApiResponse
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonPropertyName("timezone")]
        public required string Timezone { get; set; }

        [JsonPropertyName("timezone_abbreviation")]
        public required string TimezoneAbbreviation { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }

        [JsonPropertyName("hourly_units")]
        public required HourlyUnits HourlyUnits { get; set; }

        [JsonPropertyName("hourly")]
        public required Hourly Hourly { get; set; }

        [JsonPropertyName("daily_units")]
        public required DailyUnits DailyUnits { get; set; }

        [JsonPropertyName("daily")]
        public required Daily Daily { get; set; }
    }

    public class Daily
    {
        [JsonPropertyName("time")]
        public required List<string> Time { get; set; }

        [JsonPropertyName("sunrise")]
        public required List<string> Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public required List<string> Sunset { get; set; }

        [JsonPropertyName("daylight_duration")]
        public required List<double> DaylightDuration { get; set; }

        [JsonPropertyName("sunshine_duration")]
        public required List<double> SunshineDuration { get; set; }
    }

    public class DailyUnits
    {
        [JsonPropertyName("time")]
        public required string Time { get; set; }

        [JsonPropertyName("sunrise")]
        public required string Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public required string Sunset { get; set; }

        [JsonPropertyName("daylight_duration")]
        public required string DaylightDuration { get; set; }

        [JsonPropertyName("sunshine_duration")]
        public required string SunshineDuration { get; set; }
    }

    public class Hourly
    {
        [JsonPropertyName("time")]
        public required List<string> Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public required List<double> Temperature2m { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public required List<int> RelativeHumidity2m { get; set; }

        [JsonPropertyName("precipitation_probability")]
        public required List<int> PrecipitationProbability { get; set; }

        [JsonPropertyName("weather_code")]
        public required List<int> WeatherCode { get; set; }
    }

    public class HourlyUnits
    {
        [JsonPropertyName("time")]
        public required string Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public required string Temperature2m { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public required string RelativeHumidity2m { get; set; }

        [JsonPropertyName("precipitation_probability")]
        public required string PrecipitationProbability { get; set; }

        [JsonPropertyName("weather_code")]
        public required string WeatherCode { get; set; }
    }
}
