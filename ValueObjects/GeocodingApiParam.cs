using System.Web;

namespace azure_function_sample.ValueObjects
{
    public class GeocodingApiParam
    {
        public required string Address { get; set; }
        public required string ApiKey { get; set; }

        public string GetQueryString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["q"] = Address;
            query["api_key"] = ApiKey;

            return query.ToString() ?? "";
        }
    }
}
