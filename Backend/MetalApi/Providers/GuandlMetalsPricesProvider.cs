using MetalApi.GuandlModel;
using MetalReadModel;
using Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public class GuandlMetalsPricesProvider : IMetalsPricesProvider
    {
        //TODO: make readonly and from json config
        //private const string GoldPricesUrl = "https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json";
        //private const string SilverPricesUrl = "https://www.quandl.com/api/v3/datasets/LBMA/SILVER.json";
        private const string GoldPricesUrl = "WGC/GOLD_DAILY_AUD.json";
        private const string SilverPricesUrl = "LBMA/SILVER.json";
        private readonly IHttpClientFactory _httpClientFactory;

        public GuandlMetalsPricesProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MetalPrices> Get(MetalType metalType, DateTime start, DateTime end)
        {
            string json;
            var metalPrices = new MetalPrices();

            if (metalType == MetalType.Gold)
            {
                json = await GetPrices(GoldPricesUrl);
                metalPrices.Currency = Currency.AUD;
            }
            else
            {
                json = await GetPrices(SilverPricesUrl);
                metalPrices.Currency = Currency.USD;
            }

            metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.DataSource = DataSource.GuandlApi;

            return metalPrices;
        }

        private async Task<string> GetPrices(string url)
        {
            var httpClient = _httpClientFactory.CreateClient("QuandlService");//To config
            var httpResponse = await httpClient.GetAsync(url);

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
