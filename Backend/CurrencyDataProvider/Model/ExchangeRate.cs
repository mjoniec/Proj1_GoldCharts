using System;

namespace CurrencyDataProvider.Model
{
    public class ExchangeRate
    {
        public DateTime Date { get; set; } //Date time and not date for json conversions ...

        public double Rate { get; set; }
    }
}
