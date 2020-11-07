using Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public class GuandlMetalProvider : IMetalProvider
    {
        //TODO: make readonly and from json config
        //private const string GoldPricesUrl = "https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json";
        //private const string SilverPricesUrl = "https://www.quandl.com/api/v3/datasets/LBMA/SILVER.json";
        private const string GoldPricesUrl = "WGC/GOLD_DAILY_AUD.json";
        private const string SilverPricesUrl = "LBMA/SILVER.json";
        private readonly IHttpClientFactory _httpClientFactory;

        public GuandlMetalProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get(Metal metal)
        {
            string url;
            string json;
            
            if (metal == Metal.Gold)
            {
                url = GoldPricesUrl;
            }
            else
            {
                url = SilverPricesUrl;
            }

            json = await GetPrices(url);

            return json;
        }

        private async Task<string> GetPrices(string url)
        {
            var httpClient = _httpClientFactory.CreateClient("QuandlService");//To config
            var httpResponse = await httpClient.GetAsync(url);

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
