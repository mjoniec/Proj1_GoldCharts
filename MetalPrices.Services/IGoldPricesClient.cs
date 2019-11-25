using System.Threading.Tasks;

namespace MetalPrices.Services
{
    public interface IGoldPricesClient
    {
        Task<string> GetDailyGoldPrices();
    }
}
