using MetalsPrices.ExternalApi.Services.GuandlService;

namespace MetalsPrices.ExternalApi.Services.Gold
{
    public class GoldPricesExternalApiClient : GuandlMetalsPricesProviderClient
    {
        private const string ExternalMetalPricesUrl = "https://www.quandl.com/api/v3/datasets/WGC/GOLD_DAILY_AUD.json";

        public GoldPricesExternalApiClient() : base(ExternalMetalPricesUrl)
        {

        }
    }
}
