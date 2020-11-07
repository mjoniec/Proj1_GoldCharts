using System.Collections.Generic;

namespace CurrencyReadModel
{
    public class CurrencyRates
    {
        public DataSource DataSource { get; set; }

        public string BaseCurrency { get; set; }

        public string RateCurrency { get; set; }

        public List<CurrencyRateDate> Rates { get; set; }
    }
}
