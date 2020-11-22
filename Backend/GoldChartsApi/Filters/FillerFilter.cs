using CommonReadModel;
using CurrencyReadModel;
using GoldChartsApi.Model;
using MetalReadModel;
using System.Collections.Generic;
using System.Linq;

namespace GoldChartsApi.Filters
{
    /// <summary>
    /// - fill any gaps in daily prices by copying last known value
    /// </summary>
    internal class FillerFilter : IFilter
    {
        public MetalCurrencyCombined Execute(MetalCurrencyCombined metalCurrencyCombined)
        {
            metalCurrencyCombined.MetalPrices.Prices = 
                FillMissingDates(metalCurrencyCombined.MetalPrices.Prices);

            if (metalCurrencyCombined.CurrencyRates == null)
            {
                metalCurrencyCombined.CurrencyRates.Rates = 
                    FillMissingDates(metalCurrencyCombined.CurrencyRates.Rates);
            }

            return metalCurrencyCombined
        }

        private List<MetalPriceDate> FillMissingDates(List<MetalPriceDate> valuesDates)
        {
            return FillMissingDates(valuesDates
                .Cast<ValueDate>()
                .ToList())
                .Cast<MetalPriceDate>()
                .ToList();
        }

        private List<CurrencyRateDate> FillMissingDates(List<CurrencyRateDate> valuesDates)
        {
            return FillMissingDates(valuesDates
                .Cast<ValueDate>()
                .ToList())
                .Cast<CurrencyRateDate>()
                .ToList();
        }

        private List<ValueDate> FillMissingDates(List<ValueDate> valuesDates)
        {
            var list = new List<ValueDate>();
            var values = valuesDates.OrderBy(v => v.Date).ToList();
            var lastKnownValue = values.First().Value;

            for (var date = values.First().Date; date <= values.Last().Date; date = date.AddDays(1))
            {
                if (values.Any(l => l.Date.Date == date))
                {
                    lastKnownValue = values.First(l => l.Date.Date == date).Value;
                }

                list.Add(new ValueDate { Date = date, Value = lastKnownValue });
            }

            return list;
        }
    }
}
