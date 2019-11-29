using System.Threading.Tasks;

namespace MetalsPrices.Services.Gold
{
    public interface IMetalPricesService
    {
        Task StartPreparingDailyPrices();
        MetalPrices.Model.MetalPrices DailyPrices();
    }
}
