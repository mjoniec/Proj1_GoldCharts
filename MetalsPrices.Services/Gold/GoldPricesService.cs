using MetalsPrices.Abstraction.MeralPricesServices;
using System.Threading.Tasks;

namespace MetalsPrices.Services.Gold
{
    /// <summary>
    /// This service is intended to be moved to a DB maintenance solution
    /// </summary>
    public class GoldPricesService : IMetalPricesService
    {
        private readonly IMetalPricesService _metalPricesService;

        public GoldPricesService(IMetalPricesService metalPricesService)
        {
            _metalPricesService = metalPricesService;
        }

        public async Task StartPreparingPrices()
        {
            await _metalPricesService.StartPreparingPrices();
        }

        public MetalPrices.Model.MetalPrices GetPrices()
        {
            return _metalPricesService.GetPrices();
        }
    }
}
