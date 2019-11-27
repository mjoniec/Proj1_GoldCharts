using System.Net.Http;
using System.Threading.Tasks;

namespace MetalPrices.Services.Gold
{
    public class GoldPricesExternalApiClient
    {
        private readonly HttpClient _client;

        public string DailyPrices { get; private set; }

        public GoldPricesExternalApiClient()
        {
            _client = HttpClientFactory.Create();
        }

        public async Task StartDownloadingDailyGoldPrices()
        {
            var httpResponse = await _client.GetAsync("https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json");
            var httpContent = await httpResponse.Content.ReadAsStringAsync();

            DailyPrices = httpContent;
        }
    }
}
