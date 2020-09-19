using MetalsDataProvider.GuandlModel;
using MetalsDataProvider.ReadModel;
using Model;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public class FallbackMetalsPricesProvider : IMetalsPricesProvider
    {
        public async Task<MetalPrices> GetGoldPrices(DateTime start, DateTime end)
        {
            var json = await File.ReadAllTextAsync(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) 
                + "//goldPricesFallback.json");
            
            var metalPrices = GetMetalsPrices(json, start, end);

            metalPrices.Currency = Currency.AUD;

            return metalPrices;
        }

        public async Task<MetalPrices> GetSilverPrices(DateTime start, DateTime end)
        {
            var json = await File.ReadAllTextAsync(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
                + "//silverPricesFallback.json");

            var metalPrices = GetMetalsPrices(json, start, end);

            metalPrices.Currency = Currency.USD;

            return metalPrices;
        }

        private MetalPrices GetMetalsPrices(string json, DateTime start, DateTime end)
        {
            var metalPrices = json
                .Deserialize()
                .Map(start, end);

            metalPrices.DataSource = DataSource.Fallback;

            return metalPrices;
        }
    }
}
