using System.Collections.Generic;

namespace CurrencyDataProvider.Model
{
    public class ExchangeRates
    {
        public Currency baseCurrency { get; set; }

        public Currency rateCurrency { get; set; }

        public List<ExchangeRate> Rates { get; set; }
    }
}
