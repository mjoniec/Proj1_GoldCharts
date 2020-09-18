using CurrencyDataProvider.ReadModel;
using CurrencyDataProvider.Repositories;
using MetalsDataProvider.Providers;
using MetalsDataProvider.ReadModel;
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

        public async Task<MetalPrices> GetMetalPricesInCurrency(Currency currency, Metal metal)
        {
            if (metal == Metal.Gold && currency == Currency.AUD)
            {
                return await _metalsPricesProvider.GetGoldPrices();
            }

            if (metal == Metal.Gold && currency == Currency.USD)
            {
                var prices = ConvertMetalPricesToCurrency(
                    await _metalsPricesProvider.GetGoldPrices(),
                    _currenciesRepository.GetExchangeRates(Currency.AUD, Currency.USD));

                return await Task.FromResult(prices);
            }

            if (metal == Metal.Silver && currency == Currency.USD)
            {
                return await _metalsPricesProvider.GetSilverPrices();
            }

            return null;
        }

        private MetalPrices ConvertMetalPricesToCurrency(MetalPrices metalPrices, CurrencyRates rates)
        {
            var prices = new List<MetalPriceDate>();

            foreach (var p in metalPrices.Prices)
            {
                var r = rates.Rates.First(e => e.Date == p.Date);

                if (r == null) continue;

                prices.Add(new MetalPriceDate
                {
                    Date = p.Date,
                    Value = p.Value *= r.Value
                });
            }

            return new MetalPrices { Prices = prices };
        }
    }
}
