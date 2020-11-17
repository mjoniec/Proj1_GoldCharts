using CommonReadModel;
using CurrencyDataProvider.Data;
using CurrencyReadModel;
using System;
using System.Linq;

namespace CurrencyDataProvider.Providers
{
    public class CurrencyFallback : ICurrencyProvider
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end)
        {
            var exchangeRates = new CurrencyRates
            {
                DataSource = DataSource.Fallback,
                BaseCurrency = baseCurrency.ToString(),
                RateCurrency = rateCurrency.ToString()
            };

            if (baseCurrency == Currency.USD && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = USD_AUD_Data
                    .Get()
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.USD)
            {
                exchangeRates.Rates = USD_AUD_Data
                    .Get()
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();

                exchangeRates.Rates.ForEach(r => r.Value = 1.0 / r.Value);
            }

            if (baseCurrency == Currency.USD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = USD_EUR_Data
                    .Get()
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.USD)
            {
                exchangeRates.Rates = USD_EUR_Data
                    .Get()
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();

                exchangeRates.Rates.ForEach(r => r.Value = 1.0 / r.Value);
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = EUR_AUD_Data
                    .Get()
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = EUR_AUD_Data
                    .Get()
                    .Where(c => c.Date >= start && c.Date <= end)
                    .ToList();

                exchangeRates.Rates.ForEach(r => r.Value = 1.0 / r.Value);
            }

            return exchangeRates;
        }
    }
}
