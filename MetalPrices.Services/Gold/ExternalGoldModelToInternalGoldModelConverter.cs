using System;
using System.Collections.Generic;
using System.Linq;

namespace MetalPrices.Services.Gold
{
    internal class ExternalGoldModelToInternalGoldModelConverter
    {
        internal MetalPrices ConvertExternalModel(ExternalGoldDataModel externalGoldDataModel)
        {
            return new MetalPrices
            {
                Prices = GetDailyGoldPricesFromExternalData(externalGoldDataModel.Data)
                .Select(d => new MetalPriceDay
                {
                    Date = d.Key,
                    Price = d.Value
                }).ToList()
            };
        }

        private static Dictionary<DateTime, double> GetDailyGoldPricesFromExternalData(List<List<object>> data)
        {
            var dailyGoldPrices = new Dictionary<DateTime, double>();

            foreach (var dayData in data)
            {
                if (!DateTime.TryParse(dayData.First().ToString(), out DateTime key) ||
                    !double.TryParse(dayData.Last().ToString(), out double value)) continue;

                dailyGoldPrices.Add(key, value);
            }

            return dailyGoldPrices;
        }
    }
}
