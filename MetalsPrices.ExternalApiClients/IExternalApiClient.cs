using System.Threading.Tasks;

namespace MetalsPrices.ExternalApiClients
{
    public interface IExternalApiClient
    {
        Task StartDownloadingPrices();
        string Prices { get; }
    }
}
