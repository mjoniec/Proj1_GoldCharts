using MetalReadModel;
using System;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public interface IMetalsPricesProvider
    {
        Task<MetalPrices> GetGoldPrices(DateTime start, DateTime end);
        Task<MetalPrices> GetSilverPrices(DateTime start, DateTime end);
    }
}
