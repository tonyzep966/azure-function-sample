using System.Text.Json.Serialization;

namespace azure_function_sample.ValueObjects
{
    public class TimeApiResponseModel
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("day")]
        public int Day { get; set; }

        [JsonPropertyName("hour")]
        public int Hour { get; set; }

        [JsonPropertyName("minute")]
        public int Minute { get; set; }

        [JsonPropertyName("seconds")]
        public int Seconds { get; set; }

        [JsonPropertyName("milliSeconds")]
        public int MilliSeconds { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("date")]
        public required string Date { get; set; }

        [JsonPropertyName("time")]
        public required string Time { get; set; }

        [JsonPropertyName("timeZone")]
        public required string TimeZone { get; set; }

        [JsonPropertyName("dayOfWeek")]
        public required string DayOfWeek { get; set; }

        [JsonPropertyName("dstActive")]
        public bool DstActive { get; set; }
    }
}
