using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public interface IMetalsPricesDataProvider
    {
        Task<string> GetGoldPrices();
        Task<string> GetSilverPrices();
    }
}
