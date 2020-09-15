using MetalsDataProvider.ReadModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetalsDataProvider.Providers
{
    public class FallbackMetalsPricesProvider : IMetalsPricesProvider
    {
        //TODO: fallback data read from some static Json
        public async Task<MetalPrices> GetGoldPrices()
        {
            return await Task.FromResult(new MetalPrices 
            { 
                Prices = new List<MetalPriceDateTime>
                {
                    new MetalPriceDateTime
                    {
                        DateTime = new DateTime(2020, 1, 1),
                        Price = 1000.0
                    },
                    new MetalPriceDateTime
                    {
                        DateTime = new DateTime(2020, 1, 2),
                        Price = 2000.0
                    }
                }
            });
        }

        public async Task<MetalPrices> GetSilverPrices()
        {
            return await Task.FromResult(new MetalPrices
            {
                Prices = new List<MetalPriceDateTime>
                {
                    new MetalPriceDateTime
                    {
                        DateTime = new DateTime(2020, 1, 1),
                        Price = 100.0
                    },
                    new MetalPriceDateTime
                    {
                        DateTime = new DateTime(2020, 1, 2),
                        Price = 200.0
                    }
                }
            });
        }
    }
}
