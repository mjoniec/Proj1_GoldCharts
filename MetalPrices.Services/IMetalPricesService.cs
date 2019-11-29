using System.Threading.Tasks;

namespace MetalPrices.Services.Gold
{
    public interface IMetalPricesService
    {
        Task StartPreparingDailyPrices();
        Model.MetalPrices DailyPrices();
    }
}
