using CurrencyDataProvider.ReadModel;
using System;
using System.Collections.Generic;

namespace CurrencyDataProvider.Repositories
{
    public class FallbackCurrenciesRepository : ICurrenciesRepository
    {
        public ExchangeRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency)
        {
            var exchangeRates = new ExchangeRates
            {
                BaseCurrency = baseCurrency.ToString(),
                RateCurrency = rateCurrency.ToString()
            };

            if (baseCurrency == Currency.USD && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = new List<ExchangeRate>
                {
                    new ExchangeRate{ Date = new DateTime(2020, 1, 1), Rate = 1.1 },
                    new ExchangeRate{ Date = new DateTime(2020, 1, 2), Rate = 1.2 }
                };
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.USD)
            {
                exchangeRates.Rates = new List<ExchangeRate>
                {
                    new ExchangeRate{ Date = new DateTime(2020, 1, 1), Rate = 0.9 },
                    new ExchangeRate{ Date = new DateTime(2020, 1, 2), Rate = 0.8 }
                };
            }

            if (baseCurrency == Currency.USD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = new List<ExchangeRate>
                {
                    new ExchangeRate{ Date = new DateTime(2020, 1, 1), Rate = 2.1 },
                    new ExchangeRate{ Date = new DateTime(2020, 1, 2), Rate = 2.2 }
                };
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.USD)
            {
                exchangeRates.Rates = new List<ExchangeRate>
                {
                    new ExchangeRate{ Date = new DateTime(2020, 1, 1), Rate = 0.6 },
                    new ExchangeRate{ Date = new DateTime(2020, 1, 2), Rate = 0.5 }
                };
            }

            return exchangeRates;
        }
    }
}
