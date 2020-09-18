using System.Collections.Generic;

namespace CurrencyDataProvider.ReadModel
{
    public class CurrencyRates
    {
        public string BaseCurrency { get; set; }

        public string RateCurrency { get; set; }

        public List<CurrencyRateDate> Rates { get; set; }
    }
}
