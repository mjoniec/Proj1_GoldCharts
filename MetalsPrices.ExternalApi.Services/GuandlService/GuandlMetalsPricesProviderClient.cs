using MetalsPrices.Abstraction.MetalPricesDataProviders;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetalsPrices.ExternalApi.Services.GuandlService
{
    public abstract class GuandlMetalsPricesProviderClient : IMetalsPricesDataProvider
    {
        private readonly string _externalMetalPricesUrl;
        private readonly HttpClient _client;
        private string _dailyPrices; //different name provides additional info on structure

        public string Prices => _dailyPrices;

        public GuandlMetalsPricesProviderClient(string externalMetalPricesUrl)
        {
            _client = HttpClientFactory.Create();
            _externalMetalPricesUrl = externalMetalPricesUrl;
        }

        public async Task StartPreparingPrices()
        {
            var httpResponse = await _client.GetAsync(_externalMetalPricesUrl);
            var httpContent = await httpResponse.Content.ReadAsStringAsync();

            _dailyPrices = httpContent;
        }
    }
}
