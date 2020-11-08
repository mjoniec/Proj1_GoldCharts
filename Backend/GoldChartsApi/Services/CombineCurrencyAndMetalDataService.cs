using CommonReadModel;
using CurrencyReadModel;
using MetalReadModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoldChartsApi.Services
{
    public class CombineCurrencyAndMetalDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CombineCurrencyAndMetalDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MetalPrices> GetMetalPricesInCurrency(
            Currency currency, 
            Metal metal,
            DateTime start,
            DateTime end)
        {
            return await GetMetalPrices(metal, start, end);

            //if (metal == MetalType.Gold && currency == Currency.USD)
            //{
            //    var prices = ConvertMetalPricesToCurrency(
            //        await _metalsPricesProvider.GetGoldPrices(start, end),
            //        _currenciesRepository.GetExchangeRates(Currency.AUD, Currency.USD, start, end));

            //    return await Task.FromResult(prices);
            //}
        }

        private async Task<MetalPrices> GetMetalPrices(Metal metal, DateTime start, DateTime end)
        {
            var url = metal.ToString() + "/" + start.ToString("yyyy-MM-dd") + "/" + end.ToString("yyyy-MM-dd");
            var httpClient = _httpClientFactory.CreateClient("MetalApi");
            var httpResponse = await httpClient.GetAsync(url);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var metalPrices = JsonConvert.DeserializeObject<MetalPrices>(json);

            return metalPrices;
        }

        private MetalPrices ConvertMetalPricesToCurrency(MetalPrices metalPrices, CurrencyRates currencyRates)
        {
            var metalPricesFilled = FillMissingDates(metalPrices.Prices);
            var ratesFilled = FillMissingDates(currencyRates.Rates);
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

            return new MetalPrices 
            { 
                DataSource = metalPrices.DataSource, 
                Currency = metalPrices.Currency, 
                Prices = prices 
            };
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
