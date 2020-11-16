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
    /// <summary>
    /// Responsibility of this service is to:
    /// - sent an http request to an api services 
    /// - get metal prices data daily
    /// - get currency exchange rates data daily
    /// - fill any gaps in daily prices
    /// - mesh metal and currency data to match requested currency and metal 
    /// - filter results by start end dates
    /// TODO: maybe some refactor as it violiates Single Responsibility Principle ?
    /// </summary>
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
            //unknow in which currency metal service is going to provide data
            var metalPrices = await GetMetalPrices(metal, start, end);

            //no need to get exchange rate to calculate metal price in different currency
            if (metalPrices.Currency == currency)
            {
                //TODO figure a better cast
                var metalPricesFilled = new List<MetalPriceDate>();

                foreach (var p in FillMissingDates(metalPrices.Prices))
                {
                    metalPricesFilled.Add(new MetalPriceDate
                    {
                        Date = p.Date,
                        Value = p.Value
                    });
                }

                return new MetalPrices
                {
                    Currency = metalPrices.Currency,
                    DataSource = metalPrices.DataSource,
                    Prices = metalPricesFilled
                };
            }
            
            var currencyRates = await GetCurrencyRates(metalPrices.Currency, currency, start, end);
            var metalPricesConverted = ConvertMetalPricesToCurrency(metalPrices, currencyRates);

            return metalPricesConverted;
        }

        private async Task<CurrencyRates> GetCurrencyRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end)
        {
            var url = baseCurrency.ToString() + "/" + rateCurrency.ToString() + "/" + start.ToString("yyyy-MM-dd") + "/" + end.ToString("yyyy-MM-dd");
            var httpClient = _httpClientFactory.CreateClient(CurrencyApiOptions.CurrencyApi);
            var httpResponse = await httpClient.GetAsync(url);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var currencyRates = JsonConvert.DeserializeObject<CurrencyRates>(json);

            return currencyRates;
        }

        private async Task<MetalPrices> GetMetalPrices(Metal metal, DateTime start, DateTime end)
        {
            var url = metal.ToString() + "/" + start.ToString("yyyy-MM-dd") + "/" + end.ToString("yyyy-MM-dd");
            var httpClient = _httpClientFactory.CreateClient(MetalApiOptions.MetalApi);
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
