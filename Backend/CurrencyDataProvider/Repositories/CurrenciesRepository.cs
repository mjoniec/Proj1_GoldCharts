using CurrencyDataProvider.ReadModel;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyDataProvider.Repositories
{
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private readonly CurrencyContext _context;

        public CurrenciesRepository(CurrencyContext context)
        {
            _context = context;
        }

        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency)
        {
            var exchangeRates = new CurrencyRates
            {
                BaseCurrency = baseCurrency.ToString(),
                RateCurrency = rateCurrency.ToString()
            };

            if (baseCurrency == Currency.USD && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = new List<CurrencyRateDate>(_context.USD_AUD);
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.USD)
            {
                var AUD_USD = _context.USD_AUD.ToList();

                AUD_USD.ForEach(e => e.Value = 1 / e.Value);

                exchangeRates.Rates = new List<CurrencyRateDate>(AUD_USD);
            }

            if (baseCurrency == Currency.USD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = new List<CurrencyRateDate>(_context.USD_EUR);
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.USD)
            {
                var EUR_USD = _context.USD_EUR.ToList();

                EUR_USD.ForEach(e => e.Value = 1 / e.Value);

                exchangeRates.Rates = new List<CurrencyRateDate>(EUR_USD);
            }

            return exchangeRates;
        }
    }
}
