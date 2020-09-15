using CurrencyDataProvider.ReadModel;
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

        public ExchangeRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency)
        {
            var exchangeRates = new ExchangeRates
            {
                BaseCurrency = baseCurrency.ToString(),
                RateCurrency = rateCurrency.ToString()
            };

            if (baseCurrency == Currency.USD && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = new List<ExchangeRate>(_context.USD_AUD);
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.USD)
            {
                var AUD_USD = _context.USD_AUD.ToList();

                AUD_USD.ForEach(e => e.Rate = 1 / e.Rate);

                exchangeRates.Rates = new List<ExchangeRate>(AUD_USD);
            }

            if (baseCurrency == Currency.USD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = new List<ExchangeRate>(_context.USD_EUR);
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.USD)
            {
                var EUR_USD = _context.USD_EUR.ToList();

                EUR_USD.ForEach(e => e.Rate = 1 / e.Rate);

                exchangeRates.Rates = new List<ExchangeRate>(EUR_USD);
            }

            return exchangeRates;
        }
    }
}
