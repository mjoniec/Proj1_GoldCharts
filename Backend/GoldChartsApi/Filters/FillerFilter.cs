using CommonReadModel;
using GoldChartsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldChartsApi.Filters
{
    /// <summary>
    /// - fill any gaps in daily prices by copying last known value
    /// </summary>
    public class FillerFilter : IFilter
    {
        public MetalCurrencyCombined Execute(MetalCurrencyCombined metalCurrencyCombined)
        {
            var metalPricesFilled =
                FillMissingDates(metalCurrencyCombined.MetalPrices.Prices,
                    metalCurrencyCombined.Start, metalCurrencyCombined.End);

            metalCurrencyCombined.MetalPrices.Prices = metalPricesFilled;

            if (metalCurrencyCombined.CurrencyRates != null)
            {
                var currencyRatesFilled =
                    FillMissingDates(metalCurrencyCombined.CurrencyRates.Rates,
                        metalCurrencyCombined.Start, metalCurrencyCombined.End);

                metalCurrencyCombined.CurrencyRates.Rates = currencyRatesFilled;
            }

            return metalCurrencyCombined;
        }

        private List<ValueDate> FillMissingDates(List<ValueDate> valuesDates,
            DateTime start, DateTime end)
        {
            var list = new List<ValueDate>();
            var lastKnownValue = valuesDates.OrderBy(v => v.Date).First().Value;
            //var lastKnownValue = values.First().Value;

            //fills with first known value all preceding from start if they are not provided
            //fills any daily gaps with last known value

            for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
            {
                var vd = valuesDates.FirstOrDefault(l => l.Date.Date == date);

                if(vd != null)
                {
                    lastKnownValue = vd.Value;
                }

                list.Add(new ValueDate { Date = date, Value = lastKnownValue });
            }

            return list;
        }
    }
}
