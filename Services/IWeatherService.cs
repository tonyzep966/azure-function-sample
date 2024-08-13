using azure_function_sample.ValueObjects;

namespace azure_function_sample.Services
{
    public interface IWeatherService
    {
        Task<OpenMeteoApiResponse?> GetWeatherByAddress(string address);
    }
}
