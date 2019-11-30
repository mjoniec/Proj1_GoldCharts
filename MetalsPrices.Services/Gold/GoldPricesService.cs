using MetalsPrices.ExternalApiClients;
using MetalsPrices.ExternalApiClients.Gold;
using System.Threading.Tasks;

namespace MetalsPrices.Services.Gold
{
    public class GoldPricesService : IMetalPricesService
    {
        private readonly IExternalApiClient _goldPricesExternalApiClient;
        private readonly ExternalGoldDataJsonDeserializer _externalGoldDataJsonDeserializer;
        private readonly ExternalGoldModelToInternalGoldModelConverter _externalGoldModelToInternalGoldModelConverter;

        public GoldPricesService()
        {
            _goldPricesExternalApiClient = new GoldPricesExternalApiClient();
            _externalGoldDataJsonDeserializer = new ExternalGoldDataJsonDeserializer();
            _externalGoldModelToInternalGoldModelConverter = new ExternalGoldModelToInternalGoldModelConverter();
        }

        public async Task StartPreparingPrices()
        {
            await _goldPricesExternalApiClient.StartDownloadingPrices();
        }

        public MetalPrices.Model.MetalPrices GetPrices()
        {
            var dailyPrices = _goldPricesExternalApiClient.Prices;

            if (dailyPrices == null) return null;

            var externalGoldModel = _externalGoldDataJsonDeserializer.DeserializeDataFromMessage(dailyPrices);

            if (externalGoldModel == null) return null;

            var goldModel = _externalGoldModelToInternalGoldModelConverter.ConvertExternalModel(externalGoldModel);

            return goldModel;
        }
    }
}
