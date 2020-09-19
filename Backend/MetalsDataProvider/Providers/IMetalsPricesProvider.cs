using MetalsDataProvider.ReadModel;
using System;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public interface IMetalsPricesProvider
    {
        Task<MetalPrices> GetGoldPrices(DateTime start, DateTime end);
        Task<MetalPrices> GetSilverPrices(DateTime start, DateTime end);
    }
}
