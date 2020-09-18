using System;

namespace CurrencyDataProvider.ReadModel
{
    public class CurrencyRateDate
    {
        public DateTime Date { get; set; } //Date time and not date for json conversions ...

        public double Value { get; set; }
    }
}
