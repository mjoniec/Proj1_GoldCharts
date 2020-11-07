using CommonReadModel;
using CurrencyReadModel;
using System;

namespace CurrencyDataProvider.Providers
{
    public interface ICurrencyProvider
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end);
    }
}
