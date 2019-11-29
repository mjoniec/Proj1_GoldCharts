using System.Threading.Tasks;

namespace MetalPrices.Services.Gold
{
    public class GoldPricesService : IMetalPricesService
    {
        private readonly GoldPricesExternalApiClient _goldPricesExternalApiClient;
        private readonly ExternalGoldDataJsonDeserializer _externalGoldDataJsonDeserializer;
        private readonly ExternalGoldModelToInternalGoldModelConverter _externalGoldModelToInternalGoldModelConverter;

        public GoldPricesService()
        {
            _goldPricesExternalApiClient = new GoldPricesExternalApiClient();
            _externalGoldDataJsonDeserializer = new ExternalGoldDataJsonDeserializer();
            _externalGoldModelToInternalGoldModelConverter = new ExternalGoldModelToInternalGoldModelConverter();
        }

        public async Task StartPreparingDailyPrices()
        {
            await _goldPricesExternalApiClient.StartDownloadingDailyGoldPrices();
        }

        public Model.MetalPrices DailyPrices()
        {
            var dailyPrices = _goldPricesExternalApiClient.DailyPrices;

            if (dailyPrices == null) return null;

            var externalGoldModel = _externalGoldDataJsonDeserializer.DeserializeDataFromMessage(dailyPrices);

            if (externalGoldModel == null) return null;

            var goldModel = _externalGoldModelToInternalGoldModelConverter.ConvertExternalModel(externalGoldModel);

            return goldModel;
        }
    }
}
