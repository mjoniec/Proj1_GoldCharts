using System.Threading.Tasks;

namespace MetalsPrices.Abstraction.MetalPricesDataProviders
{
    public interface IMetalsPricesDataProvider
    {
        Task StartPreparingPrices();
        string Prices { get; }
    }
}
