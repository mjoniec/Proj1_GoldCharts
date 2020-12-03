using CommonModel;
using MetalReadModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetalApi.GuandlModel
{
    internal static partial class GuandlMetalModelExtensions
    {
        internal static GuandlMetalModel Deserialize(this string json)
        {
            var metalDataJson = ExtractDailyMetalPricesFromExternalJson(json);
            var metalData = JsonConvert.DeserializeObject<GuandlMetalModel>(metalDataJson);

            return metalData;
        }

        internal static MetalPrices Map(this GuandlMetalModel guandlMetalModel, DateTime start, DateTime end)
        {
            return new MetalPrices
            {
                //quite some internal know how here ... - refactor it somehow?

                Prices = GetDailyGoldPricesFromExternalData(guandlMetalModel.Data)
                    .Select(d => new ValueDate
                    {
                        Date = d.Key,
                        Value = d.Value
                    })
                    .Where(m => m.Date >= start && m.Date <= end)
                    .ToList(),
                
                DataSource = guandlMetalModel.Name.Contains("Fallback")
                    ? DataSource.Fallback 
                    : DataSource.GuandlApi,
                
                Currency = guandlMetalModel.Name.Contains("Silver")
                    ? Currency.USD 
                    : (guandlMetalModel.Name.Contains("Gold")
                        ? Currency.AUD 
                        : Currency.EUR)//this does not feel right
            };
        }

        private static Dictionary<DateTime, double> GetDailyGoldPricesFromExternalData(List<List<object>> data)
        {
            var dailyGoldPrices = new Dictionary<DateTime, double>();

            foreach (var dayData in data)
            {
                if (dayData.Any(d => d == null) ||
                    !DateTime.TryParse(dayData[0].ToString(), out DateTime key) ||
                    !double.TryParse(dayData[1].ToString(), out double value)) continue;

                dailyGoldPrices.Add(key, value);
            }

            return dailyGoldPrices;
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
