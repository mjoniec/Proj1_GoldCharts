using System;
using System.Collections.Generic;

namespace MetalPrices.Services.Gold
{
    /// <summary>
    /// To be replaced with nuget
    /// </summary>
    public class MetalPrices
    {
        public List<MetalPriceDay> Prices { get; set; }
    }

    public class MetalPriceDay
    {
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
