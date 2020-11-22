using GoldChartsApi.Model;

namespace GoldChartsApi.Filters
{
    internal interface IFilter
    {
        MetalCurrencyCombined Execute(MetalCurrencyCombined metalCurrencyCombined);
    }
}
