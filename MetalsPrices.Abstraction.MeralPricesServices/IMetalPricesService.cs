using System.Threading.Tasks;

namespace MetalsPrices.Abstraction.MeralPricesServices
{
    public interface IMetalPricesService
    {
        Task StartPreparingPrices();
        MetalPrices.Model.MetalPrices GetPrices();
    }
}
