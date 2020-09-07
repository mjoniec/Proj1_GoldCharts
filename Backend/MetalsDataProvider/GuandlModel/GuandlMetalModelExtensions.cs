using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MetalsDataProvider.GuandlModel
{
    internal static partial class GuandlMetalModelExtensions
    {
        internal static GuandlMetalDataModel Deserialize(string json)
        {
            var metalDataJson = ExtractDailyMetalPricesFromExternalJson(json);
            var metalData = JsonConvert.DeserializeObject<GuandlMetalDataModel>(metalDataJson);

            return metalData;
        }

        private static string ExtractDailyMetalPricesFromExternalJson(string json)
        {
            var allChildren = AllChildren(JObject.Parse(json));

            var metalDataJsonata = allChildren
                .First(c => c.Path.Contains("dataset"))
                .Children<JObject>()
                .First()
                .ToString();

            return metalDataJsonata;
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
