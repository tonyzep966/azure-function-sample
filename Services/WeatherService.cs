using azure_function_sample.ValueObjects;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace azure_function_sample.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _geocodingApiUrl = "https://geocode.maps.co/search";
        private readonly string _geocodingApiKey = Environment.GetEnvironmentVariable("GEOCODING_API_KEY") ?? throw new ArgumentNullException(nameof(_geocodingApiKey));
        /**
         * <summary>Get the current time of a time zone by geographical coordinates</summary>
         */
        private readonly string _timeApiUri = "https://timeapi.io/api/Time/current/coordinate";
        private readonly string _openMeteoWeatherForecastApiUri = "https://api.open-meteo.com/v1/forecast";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OpenMeteoApiResponse?> GetWeatherByAddress(string address)
        {
            var geocodingResponse = await GetGeocodingByAddressAsync(address) ?? throw new Exception("Geocoding API response is null");
            var timeResponse = await GetTimeByGeoCoordinateAsync(geocodingResponse.Lat, geocodingResponse.Lon) ?? throw new Exception("Time API response is null");
            var weatherForecastResponse = await GetWeatherForecastByCoordinateAndTimezone(geocodingResponse.Lat, geocodingResponse.Lon, timeResponse.TimeZone);
            return weatherForecastResponse;
        }

        private async Task<GeocodingApiResponse?> GetGeocodingByAddressAsync(string address)
        {
            var geocodingApiParamModel = new GeocodingApiParam
            {
                Address = address,
                ApiKey = _geocodingApiKey
            };

            var queryString = geocodingApiParamModel.GetQueryString();
            var response = await _httpClient.GetAsync($"{_geocodingApiUrl}?{queryString}");
            var content = await response.Content.ReadAsStringAsync();
            var responseBody = JsonSerializer.Deserialize<List<GeocodingApiResponse>>(content);

            return responseBody?.FirstOrDefault();
        }

        private async Task<TimeApiResponse?> GetTimeByGeoCoordinateAsync(string latitude, string longitude)
        {
            var httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_timeApiUri}?latitude={latitude}&longitude={longitude}"),
            };

            var response = await _httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();
            var responseBody = JsonSerializer.Deserialize<TimeApiResponse>(content);

            return responseBody;
        }

        private async Task<OpenMeteoApiResponse?> GetWeatherForecastByCoordinateAndTimezone(string latitude, string longitude, string timezone)
        {
            var queryParam = new Dictionary<string, string?>
            {
                { "latitude", latitude },
                { "longitude", longitude },
                { "timezone", timezone },
                { "hourly", "temperature_2m,relative_humidity_2m,precipitation_probability,weather_code" },
                { "daily", "sunrise,sunset,daylight_duration,sunshine_duration" }
            };

            var queryString = _openMeteoWeatherForecastApiUri + QueryString.Create(queryParam);

            var response = await _httpClient.GetAsync(queryString);
            var content = await response.Content.ReadAsStringAsync();
            var responseBody = JsonSerializer.Deserialize<OpenMeteoApiResponse>(content);
            return responseBody;
        }
    }
}
