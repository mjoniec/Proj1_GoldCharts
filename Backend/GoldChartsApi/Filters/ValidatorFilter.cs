using GoldChartsApi.Model;
using System;

namespace GoldChartsApi.Filters
{
    //TODO: implement better data validation than error throwing
    /// <summary>
    /// - throws if metal or currency data not loaded as expected
    /// </summary>
    public class ValidatorFilter : IFilter
    {
        public MetalCurrencyCombined Execute(MetalCurrencyCombined metalCurrencyCombined)
        {
            if(metalCurrencyCombined.MetalPrices == null)
            {
                throw new Exception("metal data not provided");
            }

            //currency exchange rates can be null if metal is already in expected currency
            if (metalCurrencyCombined.MetalPrices.Currency != metalCurrencyCombined.Currency &&
                metalCurrencyCombined.CurrencyRates == null)
            {
                throw new Exception("currency data not provided when needed");
            }
            
            return metalCurrencyCombined;
        }
    }
}
