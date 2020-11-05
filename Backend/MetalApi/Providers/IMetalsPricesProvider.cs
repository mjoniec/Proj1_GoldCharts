using Model;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public interface IMetalsPricesProvider
    {
        Task<string> Get(MetalType metalType);
        //Task<MetalPrices> Get(MetalType metalType, DateTime start, DateTime end);
    }
}
