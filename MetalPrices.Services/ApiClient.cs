using System.Net.Http;

namespace MetalPrices.Services
{
    public abstract class ApiClient
    {
        protected readonly HttpClient _client;

        public ApiClient()
        {
            _client = HttpClientFactory.Create();
        }
    }
}
