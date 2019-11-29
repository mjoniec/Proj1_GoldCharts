using System.Threading.Tasks;

namespace MetalsPrices.Services.Gold
{
    public interface IMetalPricesService
    {
        Task StartPreparingDailyPrices();
        Model.MetalPrices DailyPrices();
    }
}
