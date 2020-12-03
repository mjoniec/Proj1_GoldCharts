using CommonModel;
using CurrencyReadModel;
using System;

namespace CurrencyApi.Repositories
{
    public interface ICurrencyRepository
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end);
    }
}
