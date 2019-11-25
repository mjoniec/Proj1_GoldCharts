using System.Threading.Tasks;

namespace MetalPrices.Services
{
    public class GoldPricesClient : ApiClient, IGoldPricesClient
    {
        public string DailyGoldPrices { get; private set; }

        public async Task StartDownloadingDailyGoldPrices()
        {
            var httpResponse = await _client.GetAsync("https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json");
            var httpContent = await httpResponse.Content.ReadAsStringAsync();

            DailyGoldPrices = httpContent;
        }
    }
}
