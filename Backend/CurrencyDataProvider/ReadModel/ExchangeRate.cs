using System;

namespace CurrencyDataProvider.ReadModel
{
    public class ExchangeRate
    {
        public DateTime Date { get; set; } //Date time and not date for json conversions ...

        public double Rate { get; set; }
    }
}
