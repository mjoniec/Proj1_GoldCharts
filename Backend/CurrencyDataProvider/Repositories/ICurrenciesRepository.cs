using CurrencyDataProvider.ReadModel;
using Model;

namespace CurrencyDataProvider.Repositories
{
    public interface ICurrenciesRepository
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency);
    }
}
