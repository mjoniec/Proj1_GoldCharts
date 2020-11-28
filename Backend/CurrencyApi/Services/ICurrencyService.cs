using CommonReadModel;
using CurrencyReadModel;
using System;

namespace CurrencyApi.Services
{
    public interface ICurrencyService
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end);
    }
}
