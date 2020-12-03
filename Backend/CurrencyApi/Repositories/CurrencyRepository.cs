using CommonReadModel;
using CurrencyDatabaseCommon;
using CurrencyReadModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyApi.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyContext _context;

        public CurrencyRepository(IServiceProvider serviceProvider)
        {
            _context = (CurrencyContext)serviceProvider.GetService(typeof(CurrencyContext));
        }

        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end)
        {
            if (!_context.Database.CanConnect())
            {
                throw new Exception("Can not connect to database");
            }

            var exchangeRates = new CurrencyRates
            {
                DataSource = DataSource.Database,
                BaseCurrency = baseCurrency.ToString(),
                RateCurrency = rateCurrency.ToString()
            };

            if (baseCurrency == Currency.USD && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = new List<CurrencyRateDate>(
                    _context.USD_AUD.Where(c => c.Date >= start && c.Date <= end));
            }

            else if (baseCurrency == Currency.AUD && rateCurrency == Currency.USD)
            {
                var AUD_USD = _context.USD_AUD
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();

                AUD_USD.ForEach(e => e.Value = 1 / e.Value);

                exchangeRates.Rates = new List<CurrencyRateDate>(AUD_USD);
            }

            else if (baseCurrency == Currency.USD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = new List<CurrencyRateDate>(_context.USD_EUR
                    .Where(c => c.Date >= start && c.Date <= end));
            }

            else if (baseCurrency == Currency.EUR && rateCurrency == Currency.USD)
            {
                var EUR_USD = _context.USD_EUR
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();

                EUR_USD.ForEach(e => e.Value = 1 / e.Value);

                exchangeRates.Rates = new List<CurrencyRateDate>(EUR_USD);
            }

            else if (baseCurrency == Currency.EUR && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = new List<CurrencyRateDate>(_context.EUR_AUD
                    .Where(c => c.Date >= start && c.Date <= end));
            }

            else if (baseCurrency == Currency.AUD && rateCurrency == Currency.EUR)
            {
                var AUD_EUR = _context.EUR_AUD
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();

                AUD_EUR.ForEach(e => e.Value = 1 / e.Value);

                exchangeRates.Rates = new List<CurrencyRateDate>(AUD_EUR);
            }

            return exchangeRates;
        }
    }
}
