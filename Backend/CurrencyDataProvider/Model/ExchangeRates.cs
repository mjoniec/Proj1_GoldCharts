using System.Collections.Generic;

namespace CurrencyDataProvider.Model
{
    public class ExchangeRates
    {
        public string baseCurrency { get; set; }

        public string rateCurrency { get; set; }

        public List<ExchangeRate> Rates { get; set; }
    }
}
