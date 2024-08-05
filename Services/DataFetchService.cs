using azure_function_sample.ValueObjects;
using System.Text.Json;

namespace azure_function_sample.Services
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _geocodingApiUrl = "https://geocode.maps.co/search";
        private readonly string _geocodingApiKey = "";
        /**
         * <summary>Get the current time of a time zone by geographical coordinates</summary>
         */
        private readonly string _timeApiUri = "https://timeapi.io/api/Time/current/coordinate";

        public DataFetchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeocodingApiResponse?> GetGeocodingAsync(string address)
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
    }
}
