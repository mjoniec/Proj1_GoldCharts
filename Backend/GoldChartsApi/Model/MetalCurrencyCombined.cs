using CommonReadModel;
using CurrencyReadModel;
using MetalReadModel;
using System;
using System.Text;

namespace GoldChartsApi.Model
{
    internal class MetalCurrencyCombined
    {
        internal Currency Currency { get; set; }
        internal Metal Metal { get; set; }
        internal DateTime Start { get; set; }
        internal DateTime End { get; set; }
        internal MetalPrices MetalPrices { get; set; }
        internal CurrencyRates CurrencyRates { get; set; }
        internal StringBuilder OperationStatus { get; set; }
    }
}
