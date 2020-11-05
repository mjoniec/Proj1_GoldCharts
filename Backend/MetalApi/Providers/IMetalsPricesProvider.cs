using MetalReadModel;
using Model;
using System;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public interface IMetalsPricesProvider
    {
        Task<MetalPrices> Get(MetalType metalType);
        Task<MetalPrices> Get(MetalType metalType, DateTime start, DateTime end);
    }
}
