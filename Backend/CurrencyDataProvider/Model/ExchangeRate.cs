using System;

namespace CurrencyDataProvider.Model
{
    public class ExchangeRate
    {
        public Currency baseCurrency { get; set; }
        
        public Currency rateCurrency { get; set; }
        
        public double Rate { get; set; }

        public DateTime Date { get; set; } //Date time for json conversions ...
    }
}
