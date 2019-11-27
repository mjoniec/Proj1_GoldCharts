using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MetalPrices.Services.Gold
{
    internal class ExternalGoldDataJsonDeserializer
    {
        internal ExternalGoldDataModel DeserializeDataFromMessage(string message)
        {
            var goldDataJson = ExtractDailyGoldPricesFromExternalJson(message);
            var goldData = JsonConvert.DeserializeObject<ExternalGoldDataModel>(goldDataJson);

            return goldData;
        }

        private string ExtractDailyGoldPricesFromExternalJson(string message)
        {
            var allChildren = AllChildren(JObject.Parse(message));

            var goldData = allChildren
                .First(c => c.Path.Contains("dataset"))
                .Children<JObject>()
                .First()
                .ToString();

            return goldData;
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
