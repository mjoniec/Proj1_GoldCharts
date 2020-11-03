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

        private async Task<string> GetPrices(string url)
        {
            var httpClient = _httpClientFactory.CreateClient("QuandlService");
            var httpResponse = await httpClient.GetAsync(url);

            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<MetalPrices> GetGoldPrices(DateTime start, DateTime end)
        {
            var json = await GetPrices(GoldPricesUrl);

            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.DataSource = DataSource.GuandlApi;
            metalPrices.Currency = Currency.AUD;

            return metalPrices;
        }

        public async Task<MetalPrices> GetSilverPrices(DateTime start, DateTime end)
        {
            var json = await GetPrices(SilverPricesUrl);

            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.DataSource = DataSource.GuandlApi;
            metalPrices.Currency = Currency.USD;

            return metalPrices;
        }
    }
}
