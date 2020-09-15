using CurrencyDataProvider.ReadModel;

namespace CurrencyDataProvider.Repositories
{
    public interface ICurrenciesRepository
    {
        public ExchangeRates GetExchangeRates(Currency baseCurrency, Currency rateCurrency);
    }
}
