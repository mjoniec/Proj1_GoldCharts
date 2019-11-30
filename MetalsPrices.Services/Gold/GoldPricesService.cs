using System.Threading.Tasks;

namespace MetalsPrices.Services.Gold
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

        public async Task StartPreparingPrices()
        {
            await _goldPricesExternalApiClient.StartDownloadingDailyGoldPrices();
        }

        public MetalPrices.Model.MetalPrices Prices()
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
