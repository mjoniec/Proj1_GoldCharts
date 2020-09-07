using MetalsDataProvider.ReadModel;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public class FallbackMetalsPricesProvider : IMetalsPricesProvider
    {
        //TODO: fallback data read from some static Json
        public async Task<MetalPrices> GetGoldPrices()
        {
            return await Task.FromResult(new MetalPrices());
        }

        public async Task<MetalPrices> GetSilverPrices()
        {
            return await Task.FromResult(new MetalPrices());
        }
    }
}
