using CommonModel;
using System.Collections.Generic;

namespace MetalReadModel
{
    public class MetalPrices
    {
        public DataSource DataSource { get; set; }
        public Currency Currency { get; set; }
        public List<ValueDate> Prices { get; set; }
    }
}
