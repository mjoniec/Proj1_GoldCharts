using CommonReadModel;
using GoldChartsApi.Model;
using MetalReadModel;
using System.Collections.Generic;
using System.Linq;

namespace GoldChartsApi.Filters
{
    /// <summary>
    /// - mesh metal and currency data to match requested currency and metal 
    /// </summary>
    internal class MetalCurrencyConverterFilter : IFilter
    {
        public MetalCurrencyCombined Execute(MetalCurrencyCombined metalCurrencyCombined)
        {
            var metalPricesFilled = metalCurrencyCombined.MetalPrices.Prices.Cast<ValueDate>().ToList();
            var ratesFilled = metalCurrencyCombined.CurrencyRates.Rates.Cast<ValueDate>().ToList();
            var prices = new List<MetalPriceDate>();

            foreach (var p in metalPricesFilled)
            {
                var r = ratesFilled.FirstOrDefault(e => e.Date == p.Date);

                if (r == null) continue;

                prices.Add(new MetalPriceDate
                {
                    Date = p.Date,
                    Value = p.Value * r.Value
                });
            }

            metalCurrencyCombined.MetalPrices = new MetalPrices
            {
                DataSource = metalCurrencyCombined.MetalPrices.DataSource,
                Currency = metalCurrencyCombined.MetalPrices.Currency,
                Prices = prices
            };

            return metalCurrencyCombined;
        }
    }
}
