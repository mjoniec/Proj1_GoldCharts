using GoldChartsApi.Model;

namespace GoldChartsApi.Filters
{
    public interface IFilter
    {
        MetalCurrencyCombined Execute(MetalCurrencyCombined metalCurrencyCombined);
    }
}
