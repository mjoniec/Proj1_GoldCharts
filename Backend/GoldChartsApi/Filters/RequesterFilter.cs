using CommonModel;
using CurrencyReadModel;
using GoldChartsApi.Model;
using MetalReadModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoldChartsApi.Filters
{
    /// <summary>
    /// - get metal and currency data daily from http apis
    /// - filter results by start end dates
    /// - throws if metal or currency data not loaded as expected
    /// </summary>
    public class RequesterFilter : IFilter
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RequesterFilter(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public MetalCurrencyCombined Execute(MetalCurrencyCombined metalCurrencyCombined)
        {
            if(metalCurrencyCombined.Start >= metalCurrencyCombined.End)
            {
                throw new Exception("start date must be before end date");
            }

            //unknow in which currency metal service is going to provide data
            metalCurrencyCombined.MetalPrices = GetMetalPrices(
                metalCurrencyCombined.Metal, 
                metalCurrencyCombined.Start, 
                metalCurrencyCombined.End)
                    .Result;

            metalCurrencyCombined.OperationStatus.AppendLine("loaded metal prices");

            //need to get exchange rate to calculate metal price in different currency
            if (metalCurrencyCombined.MetalPrices.Currency != metalCurrencyCombined.Currency)
            {
                metalCurrencyCombined.CurrencyRates = GetCurrencyRates(
                    metalCurrencyCombined.MetalPrices.Currency, 
                    metalCurrencyCombined.Currency, 
                    metalCurrencyCombined.Start, 
                    metalCurrencyCombined.End)
                        .Result;

                metalCurrencyCombined.OperationStatus.AppendLine("loaded currency exchanges");
            }
            else
            {
                metalCurrencyCombined.OperationStatus.AppendLine("no need to load currency");
            }

            if (metalCurrencyCombined.MetalPrices == null)
            {
                throw new Exception("metal data not provided");
            }

            //currency exchange rates can be null if metal is already in expected currency
            if (metalCurrencyCombined.MetalPrices.Currency != metalCurrencyCombined.Currency &&
                metalCurrencyCombined.CurrencyRates == null)
            {
                throw new Exception("currency data not provided when needed");
            }

            return metalCurrencyCombined;
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
    }
}
