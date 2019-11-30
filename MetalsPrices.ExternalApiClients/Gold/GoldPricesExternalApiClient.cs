using System.Net.Http;
using System.Threading.Tasks;

namespace MetalsPrices.ExternalApiClients.Gold
{
    public class GoldPricesExternalApiClient : IExternalApiClient
    {
        private readonly HttpClient _client;
        private string _dailyPrices; //different name provides additional info on structure

        public string Prices => _dailyPrices;

        public GoldPricesExternalApiClient()
        {
            _client = HttpClientFactory.Create();
        }

        public async Task StartDownloadingPrices()
        {
            var httpResponse = await _client.GetAsync("https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json");
            var httpContent = await httpResponse.Content.ReadAsStringAsync();

            _dailyPrices = httpContent;
        }
    }
}
