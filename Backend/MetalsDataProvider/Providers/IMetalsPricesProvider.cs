using MetalsDataProvider.ReadModel;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public interface IMetalsPricesProvider
    {
        Task<MetalPrices> GetGoldPrices();
        Task<MetalPrices> GetSilverPrices();
    }
}
