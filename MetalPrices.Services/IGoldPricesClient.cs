using System.Threading.Tasks;

namespace MetalPrices.Services
{
    public interface IGoldPricesClient
    {
        string DailyGoldPrices { get; }

        Task StartDownloadingDailyGoldPrices();
    }
}
