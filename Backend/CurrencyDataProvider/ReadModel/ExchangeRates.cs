using System.Collections.Generic;

namespace CurrencyDataProvider.ReadModel
{
    public class ExchangeRates
    {
        public string BaseCurrency { get; set; }

        public string RateCurrency { get; set; }

        public List<ExchangeRate> Rates { get; set; }
    }
}
