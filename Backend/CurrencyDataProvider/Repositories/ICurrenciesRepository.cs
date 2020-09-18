using CurrencyDataProvider.ReadModel;

namespace CurrencyDataProvider.Repositories
{
    public interface ICurrenciesRepository
    {
        public CurrencyRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency);
    }
}
