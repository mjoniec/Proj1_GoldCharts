using System.Threading.Tasks;

namespace MetalPrices.Services.Gold
{
    public interface IMetalPricesService
    {
        Task StartPreparingDailyPrices();
        MetalPrices DailyPrices();
    }
}
