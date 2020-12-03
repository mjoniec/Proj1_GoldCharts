using CommonReadModel;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public class GuandlMetalProvider : IMetalProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public GuandlMetalProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> Get(Metal metal)
        {
            string url;
            string json;
            var quandlApiOptions = new QuandlApiOptions();

            _configuration
                .GetSection(QuandlApiOptions.QuandlApi)
                .Bind(quandlApiOptions);

            if (metal == Metal.Gold)
            {
                url = quandlApiOptions.GoldPricesUrl;
            }
            else
            {
                url = quandlApiOptions.SilverPricesUrl;
            }

            json = await GetPrices(url);

            return json;
        }

        private async Task<string> GetPrices(string url)
        {
            var httpClient = _httpClientFactory.CreateClient(QuandlApiOptions.QuandlApi);
            var httpResponse = await httpClient.GetAsync(url);

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
