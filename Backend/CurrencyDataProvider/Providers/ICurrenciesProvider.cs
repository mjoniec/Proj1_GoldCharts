using CurrencyDataProvider.ReadModel;
using Model;
using System;

namespace CurrencyDataProvider.Providers
{
    public interface ICurrenciesProvider
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end);
    }
}
