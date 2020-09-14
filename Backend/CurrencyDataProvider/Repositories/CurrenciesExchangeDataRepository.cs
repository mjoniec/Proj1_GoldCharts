using CurrencyDataProvider.Model;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyDataProvider.Repositories
{
    public class CurrenciesExchangeDataRepository
    {
        private readonly CurrencyContext _context;

        public CurrenciesExchangeDataRepository(CurrencyContext context)
        {
            _context = context;
        }

        public ExchangeRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency)
        {
            if (baseCurrency == Currency.USD && rateCurrency == Currency.AUD)
            {
                return new ExchangeRates
                {
                    baseCurrency = baseCurrency.ToString(),
                    rateCurrency = rateCurrency.ToString(),
                    Rates = new List<ExchangeRate>(_context.USD_AUD)
                };
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.USD)
            {
                var AUD_USD = _context.USD_AUD.ToList();

                AUD_USD.ForEach(e => e.Rate = 1 / e.Rate);

                return new ExchangeRates
                {
                    baseCurrency = baseCurrency.ToString(),
                    rateCurrency = rateCurrency.ToString(),
                    Rates = new List<ExchangeRate>(AUD_USD)
                };
            }

            if (baseCurrency == Currency.USD && rateCurrency == Currency.EUR)
            {
                return new ExchangeRates
                {
                    baseCurrency = baseCurrency.ToString(),
                    rateCurrency = rateCurrency.ToString(),
                    Rates = new List<ExchangeRate>(_context.USD_EUR)
                };
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.USD)
            {
                var AUD_USD = _context.USD_EUR.ToList();

                AUD_USD.ForEach(e => e.Rate = 1 / e.Rate);

                return new ExchangeRates
                {
                    baseCurrency = baseCurrency.ToString(),
                    rateCurrency = rateCurrency.ToString(),
                    Rates = new List<ExchangeRate>(AUD_USD)
                };
            }

            return null;
        }
    }
}
