using CurrencyDataProvider.Model;
using CurrencyDataProvider.Repositories;
using GoldChartsApi.Model;
using MetalsDataProvider.Providers;
using MetalsDataProvider.ReadModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldChartsApi.Services
{
    public class CombineCurrencyAndMetalDataService
    {
        IMetalsPricesProvider _metalsPricesProvider;
        CurrenciesExchangeDataRepository _currenciesExchangeDataRepository;

        public CombineCurrencyAndMetalDataService(IMetalsPricesProvider metalsPricesProvider,
            CurrenciesExchangeDataRepository currenciesExchangeDataRepository)
        {
            _metalsPricesProvider = metalsPricesProvider;
            _currenciesExchangeDataRepository = currenciesExchangeDataRepository;
        }

        public async Task<MetalPrices> GetMetalpricesInCurrency(Currency currency, Metal metal)
        {
            if (metal == Metal.Gold && currency == Currency.AUD)
            {
                return await _metalsPricesProvider.GetGoldPrices();
            }

            if (metal == Metal.Gold && currency == Currency.USD)
            {
                var prices = ConvertMetalPricesToCurrency(
                    await _metalsPricesProvider.GetGoldPrices(),
                    _currenciesExchangeDataRepository.GetExchangeRates(Currency.AUD, Currency.USD));

                return await Task.FromResult(prices);
            }

            if (metal == Metal.Silver && currency == Currency.AUD)
            {
                return await _metalsPricesProvider.GetSilverPrices();
            }

            if (metal == Metal.Silver && currency == Currency.USD)
            {
                var prices = ConvertMetalPricesToCurrency(
                    await _metalsPricesProvider.GetSilverPrices(),
                    _currenciesExchangeDataRepository.GetExchangeRates(Currency.AUD, Currency.USD));

                return await Task.FromResult(prices);
            }

            return null;
        }

        private MetalPrices ConvertMetalPricesToCurrency(MetalPrices metalPrices, ExchangeRates rates)
        {
            var prices = new List<MetalPriceDateTime>();

            foreach (var p in metalPrices.Prices)
            {
                var r = rates.Rates.First(e => e.Date == p.DateTime);

                if (r == null) continue;

                prices.Add(new MetalPriceDateTime
                {
                    DateTime = p.DateTime,
                    Price = p.Price *= r.Rate
                });
            }

            return new MetalPrices { Prices = prices };
        }
    }
}
