using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public class FallbackMetalsPricesProvider : IMetalsPricesDataProvider
    {
        //TODO: fallback data read from some static Json
        public async Task<string> GetGoldPrices()
        {
            return await Task.FromResult("");
        }

        public async Task<string> GetSilverPrices()
        {
            return await Task.FromResult("");
        }
    }
}
