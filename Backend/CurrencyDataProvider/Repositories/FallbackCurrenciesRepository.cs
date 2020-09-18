using CurrencyDataProvider.Initialize;
using CurrencyDataProvider.ReadModel;

namespace CurrencyDataProvider.Repositories
{
    public class FallbackCurrenciesRepository : ICurrenciesRepository
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency)
        {
            var exchangeRates = new CurrencyRates
            {
                BaseCurrency = baseCurrency.ToString(),
                RateCurrency = rateCurrency.ToString()
            };

            if (baseCurrency == Currency.USD && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = USD_AUD_Initialize.Generate();
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.USD)
            {
                exchangeRates.Rates = USD_AUD_Initialize.Generate();
                exchangeRates.Rates.ForEach(r => r.Value = 1.0 / r.Value);
            }

            if (baseCurrency == Currency.USD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = USD_EUR_Initialize.Generate();
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.USD)
            {
                exchangeRates.Rates = USD_EUR_Initialize.Generate();
                exchangeRates.Rates.ForEach(r => r.Value = 1.0 / r.Value);
            }

            if (baseCurrency == Currency.EUR && rateCurrency == Currency.AUD)
            {
                exchangeRates.Rates = EUR_AUD_Initialize.Generate();
            }

            if (baseCurrency == Currency.AUD && rateCurrency == Currency.EUR)
            {
                exchangeRates.Rates = EUR_AUD_Initialize.Generate();
                exchangeRates.Rates.ForEach(r => r.Value = 1.0 / r.Value);
            }

            return exchangeRates;
        }
    }
}
