using System.Threading.Tasks;

namespace MetalsPrices.AbstractDataProviders
{
    public interface IMetalsPricesDataProvider
    {
        Task StartPreparingPrices();
        string Prices { get; }
    }
}
