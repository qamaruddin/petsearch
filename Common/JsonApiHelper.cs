using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetSearch.Common
{
    public class JsonApiHelper : IApiHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JsonApiHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetResult<T>(string uri)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetStreamAsync(uri);
            var serializedResult = await JsonSerializer.DeserializeAsync<T>(response);
            return serializedResult;
        }
    }
}