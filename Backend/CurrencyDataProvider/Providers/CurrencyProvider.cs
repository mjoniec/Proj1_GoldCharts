using CommonReadModel;
using CurrencyReadModel;
using System;

namespace CurrencyDataProvider.Providers
{
    public class CurrencyProvider : ICurrencyProvider
    {
        private readonly IServiceProvider _serviceProvider;        

        public CurrencyProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end)
        {
            ICurrencyProvider currencyProvider;

            try
            {
                currencyProvider = (ICurrencyProvider)_serviceProvider.GetService(typeof(CurrencyRepository));

                return currencyProvider.GetExchangeRates(baseCurrency, rateCurrency, start, end);
            }
            catch 
            {
                currencyProvider = (ICurrencyProvider)_serviceProvider.GetService(typeof(CurrencyFallback));

                return currencyProvider.GetExchangeRates(baseCurrency, rateCurrency, start, end);
            }            
        }
    }
}
