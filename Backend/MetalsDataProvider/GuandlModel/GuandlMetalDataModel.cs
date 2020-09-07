using Newtonsoft.Json;
using System.Collections.Generic;

namespace MetalsDataProvider.GuandlModel
{
    internal class GuandlMetalDataModel
    {
        //names comes from external api json structure
        internal const string NEWEST_AVAILALE_DATE = "newest_available_date";
        internal const string OLDEST_AVAILABLE_DATE = "oldest_available_date";
        internal const string DATA = "data";

        [JsonProperty(NEWEST_AVAILALE_DATE)]
        internal string NewestAvailaleDate { get; set; }

        [JsonProperty(OLDEST_AVAILABLE_DATE)]
        internal string OldestAvailableDate { get; set; }

        [JsonProperty(DATA)]
        internal List<List<object>> Data { get; set; }
    }
}
