using CommonReadModel;
using CurrencyReadModel;
using System;

namespace CurrencyDataProvider.Providers
{
    public interface ICurrenciesProvider
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end);
    }
}
