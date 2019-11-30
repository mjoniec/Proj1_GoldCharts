using System.Threading.Tasks;

namespace MetalsPrices.Services
{
    public interface IMetalPricesService
    {
        Task StartPreparingPrices();
        MetalPrices.Model.MetalPrices GetPrices();
    }
}
