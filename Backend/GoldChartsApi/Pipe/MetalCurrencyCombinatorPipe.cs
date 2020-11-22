using CommonReadModel;
using GoldChartsApi.Filters;
using GoldChartsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldChartsApi.Pipe
{
    public class MetalCurrencyCombinatorPipe
    {
        private readonly IList<IFilter> _filters;

        internal MetalCurrencyCombinatorPipe(IList<IFilter> filters)
        {
            _filters = filters;
        }

        internal async Task<MetalCurrencyCombined> GetMetalPricesInCurrency(
            Currency currency,
            Metal metal,
            DateTime start,
            DateTime end)
        {
            var metalCurrencyCombined = new MetalCurrencyCombined
            {
                Currency = currency,
                Metal = metal,
                Start = start,
                End = end
            };

            try
            {
                metalCurrencyCombined = _filters.Aggregate(metalCurrencyCombined,
                    (current, operation) => operation.Execute(current));
            }
            catch (Exception e)
            {
                metalCurrencyCombined.OperationStatus.AppendLine(e.Message);
            }

            return metalCurrencyCombined;
        }
    }
}
