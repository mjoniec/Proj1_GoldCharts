using CurrencyDataProvider.ReadModel;
using CurrencyDataProvider.Repositories;
using MetalsDataProvider.Providers;
using MetalsDataProvider.ReadModel;
using Microsoft.EntityFrameworkCore.Internal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldChartsApi.Services
{
    public class CombineCurrencyAndMetalDataService
    {
        private readonly IMetalsPricesProvider _metalsPricesProvider;
        private readonly ICurrenciesRepository _currenciesRepository;

        public CombineCurrencyAndMetalDataService(IMetalsPricesProvider metalsPricesProvider,
            ICurrenciesRepository currenciesExchangeDataRepository)
        {
            _metalsPricesProvider = metalsPricesProvider;
            _currenciesRepository = currenciesExchangeDataRepository;
        }

        public async Task<MetalPrices> GetMetalPricesInCurrency(
            Currency currency, 
            Metal metal,
            DateTime start,
            DateTime end)
        {
            if (metal == Metal.Gold && currency == Currency.AUD)
            {
                return await _metalsPricesProvider.GetGoldPrices(start, end);
            }

            if (metal == Metal.Gold && currency == Currency.USD)
            {
                var prices = ConvertMetalPricesToCurrency(
                    await _metalsPricesProvider.GetGoldPrices(start, end),
                    _currenciesRepository.GetExchangeRates(Currency.AUD, Currency.USD, start, end));

                return await Task.FromResult(prices);
            }

            if (metal == Metal.Silver && currency == Currency.USD)
            {
                return await _metalsPricesProvider.GetSilverPrices(start, end);
            }

            return null;
        }

        private MetalPrices ConvertMetalPricesToCurrency(MetalPrices metalPrices, CurrencyRates rates)
        {
            var metalPricesFilled = FillMissingDates(metalPrices.Prices);
            var ratesFilled = FillMissingDates(rates.Rates);
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

            return new MetalPrices { Prices = prices };
        }

        private List<ValueDate> FillMissingDates(List<MetalPriceDate> valuesDates)
        {
            return FillMissingDates(valuesDates.Cast<ValueDate>().ToList());
        }

        private List<ValueDate> FillMissingDates(List<CurrencyRateDate> valuesDates)
        {
            return FillMissingDates(valuesDates.Cast<ValueDate>().ToList());
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
