using CurrencyDataProvider.ReadModel;
using Model;
using System;

namespace CurrencyDataProvider.Repositories
{
    public interface ICurrenciesRepository
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end);
    }
}
