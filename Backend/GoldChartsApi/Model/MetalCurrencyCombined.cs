using CommonModel;
using CurrencyReadModel;
using MetalReadModel;
using System;
using System.Text;

namespace GoldChartsApi.Model
{
    public class MetalCurrencyCombined
    {
        public Currency Currency { get; set; }
        public Metal Metal { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public MetalPrices MetalPrices { get; set; }
        public CurrencyRates CurrencyRates { get; set; }
        public StringBuilder OperationStatus { get; set; }
    }
}
