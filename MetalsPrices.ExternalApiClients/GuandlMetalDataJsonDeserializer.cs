using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MetalsPrices.ExternalApiClients
{
    internal class GuandlMetalDataJsonDeserializer
    {
        internal GuandlMetalDataModel DeserializeDataFromMessage(string message)
        {
            var metalDataJson = ExtractDailyMetalPricesFromExternalJson(message);
            var metalData = JsonConvert.DeserializeObject<GuandlMetalDataModel>(metalDataJson);

            return metalData;
        }

        private string ExtractDailyMetalPricesFromExternalJson(string message)
        {
            var allChildren = AllChildren(JObject.Parse(message));

            var data = allChildren
                .First(c => c.Path.Contains("dataset"))
                .Children<JObject>()
                .First()
                .ToString();

            return data;
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;

                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}
