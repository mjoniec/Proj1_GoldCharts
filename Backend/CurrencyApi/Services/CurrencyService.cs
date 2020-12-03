using CommonModel;
using CurrencyApi.Repositories;
using CurrencyReadModel;
using System;

namespace CurrencyApi.Services
{
    /// <summary>
    /// Decides if use an actual repository or a fallback as data provider
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly IServiceProvider _serviceProvider;

        public CurrencyService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency, DateTime start, DateTime end)
        {
            ICurrencyRepository currencyRepository;

            try
            {
                currencyRepository = (ICurrencyRepository)_serviceProvider.GetService(typeof(CurrencyRepository));

                return currencyRepository.GetExchangeRates(baseCurrency, rateCurrency, start, end);
            }
            catch
            {
                currencyRepository = (ICurrencyRepository)_serviceProvider.GetService(typeof(CurrencyFallback));

                return currencyRepository.GetExchangeRates(baseCurrency, rateCurrency, start, end);
            }
        }
    }
}
