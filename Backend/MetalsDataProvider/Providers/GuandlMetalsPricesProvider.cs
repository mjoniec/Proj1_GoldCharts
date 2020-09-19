using MetalsDataProvider.GuandlModel;
using MetalsDataProvider.ReadModel;
using Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public class GuandlMetalsPricesProvider : IMetalsPricesProvider
    {
        //TODO: make readonly and from json config
        private const string GoldPricesUrl = "https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json";
        private const string SilverPricesUrl = "https://www.quandl.com/api/v3/datasets/LBMA/SILVER.json";

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

        public async Task<MetalPrices> GetGoldPrices(DateTime start, DateTime end)
        {
            var json = await GetPrices(GoldPricesUrl);
            
            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.Currency = Currency.AUD;

            return metalPrices;
        }

        public async Task<MetalPrices> GetSilverPrices(DateTime start, DateTime end)
        {
            var json = await GetPrices(SilverPricesUrl);

            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.Currency = Currency.USD;

            return metalPrices;
        }
    }
}
