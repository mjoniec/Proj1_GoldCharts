using MetalsPrices.Abstraction.MetalPricesDataProviders;
using System.Threading.Tasks;

namespace MetalsPrices.ExternalApiClients.Gold
{
    public class GoldPricesExternalApiService
    {
        private readonly IMetalsPricesDataProvider _goldPricesExternalApiClient;
        private readonly GuandlMetalDataJsonDeserializer _guandlMetalDataJsonDeserializer;
        private readonly GuandlMetalModelToMetalModelConverter _guandlMetalModelToMetalModelConverter;

        public GoldPricesExternalApiService()
        {
            _goldPricesExternalApiClient = new GoldPricesExternalApiClient();
            _guandlMetalDataJsonDeserializer = new GuandlMetalDataJsonDeserializer();
            _guandlMetalModelToMetalModelConverter = new GuandlMetalModelToMetalModelConverter();
        }

        public async Task StartPreparingPrices()
        {
            await _goldPricesExternalApiClient.StartPreparingPrices();
        }

        public MetalPrices.Model.MetalPrices GetPrices()
        {
            var dailyPrices = _goldPricesExternalApiClient.Prices;

            if (dailyPrices == null) return null;

            var externalGoldModel = _guandlMetalDataJsonDeserializer.DeserializeDataFromMessage(dailyPrices);

            if (externalGoldModel == null) return null;

            var goldModel = _guandlMetalModelToMetalModelConverter.ConvertExternalModel(externalGoldModel);

            return goldModel;
        }
    }
}
