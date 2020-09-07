using System.Net.Http;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public class GuandlMetalsPricesProvider : IMetalsPricesDataProvider
    {
        //TODO: make readonly and from json config
        private const string GoldPricesUrl = "https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json";
        private const string SilverPricesUrl = "";//TODO: had it stored somewhere in gold backend solution maybe...

        private readonly HttpClient _client;

        public GuandlMetalsPricesProvider()
        {
            _client = new HttpClient();
        }

        private async Task<string> GetPrices(string url)
        {
            var httpResponse = await _client.GetAsync(url);

            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<string> GetGoldPrices()
        {
            return await GetPrices(GoldPricesUrl);
        }

        public async Task<string> GetSilverPrices()
        {
            return await GetPrices(SilverPricesUrl);
        }
    }
}
