using System;
using System.Collections.Generic;
using System.Linq;

namespace MetalsPrices.ExternalApi.Services.GuandlService
{
    internal class GuandlMetalModelToMetalModelConverter
    {
        internal MetalPrices.Model.MetalPrices ConvertExternalModel(GuandlMetalDataModel guandlMetalDataModel)
        {
            return new MetalPrices.Model.MetalPrices
            {
                Prices = GetDailyGoldPricesFromExternalData(guandlMetalDataModel.Data)
                .Select(d => new MetalPrices.Model.MetalPriceDateTime
                {
                    DateTime = d.Key,
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
