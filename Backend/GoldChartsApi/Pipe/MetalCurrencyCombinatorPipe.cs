using CommonReadModel;
using GoldChartsApi.Filters;
using GoldChartsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldChartsApi.Pipe
{
    public class MetalCurrencyCombinatorPipe
    {
        private readonly IServiceProvider _serviceProvider;

        public MetalCurrencyCombinatorPipe(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal async Task<MetalCurrencyCombined> GetMetalPricesInCurrency(
            Currency currency,
            Metal metal,
            DateTime start,
            DateTime end)
        {
            //sets up order of execution
            var filters = new List<IFilter>
            {
                (IFilter)_serviceProvider.GetService(typeof(RequesterFilter)),
                (IFilter)_serviceProvider.GetService(typeof(ValidatorFilter)),
                (IFilter)_serviceProvider.GetService(typeof(FillerFilter)),
                (IFilter)_serviceProvider.GetService(typeof(MetalCurrencyConverterFilter))
            };

            var metalCurrencyCombined = new MetalCurrencyCombined
            {
                Currency = currency,
                Metal = metal,
                Start = start,
                End = end,
                OperationStatus = new StringBuilder()
            };

            try
            {
                metalCurrencyCombined = filters.Aggregate(metalCurrencyCombined,
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
