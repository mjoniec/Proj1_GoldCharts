using System.Net.Http;
using System.Threading.Tasks;

namespace MetalPrices.Services
{
    public abstract class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = HttpClientFactory.Create();
        }
    }
}
