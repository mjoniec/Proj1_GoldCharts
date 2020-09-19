using MetalsDataProvider.GuandlModel;
using MetalsDataProvider.ReadModel;
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
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var json = await File.ReadAllTextAsync(path + "//goldPricesFallback.json");

            return json
                .Deserialize()
                .Map(start, end);
        }

        public async Task<MetalPrices> GetSilverPrices(DateTime start, DateTime end)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var json = await File.ReadAllTextAsync(path + "//silverPricesFallback.json");

            return json
                .Deserialize()
                .Map(start, end);
        }
    }
}
