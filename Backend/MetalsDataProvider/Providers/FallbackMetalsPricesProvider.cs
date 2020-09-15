using MetalsDataProvider.GuandlModel;
using MetalsDataProvider.ReadModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public class FallbackMetalsPricesProvider : IMetalsPricesProvider
    {
        //TODO: fallback data read from some static Json
        public async Task<MetalPrices> GetGoldPrices()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var json = await File.ReadAllTextAsync(path + "//goldPricesFallback.json");

            return json
                .Deserialize()
                .Map();
        }

        public async Task<MetalPrices> GetSilverPrices()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var json = await File.ReadAllTextAsync(path + "//silverPricesFallback.json");

            return json
                .Deserialize()
                .Map();
        }
    }
}
