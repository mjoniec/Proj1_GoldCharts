using Newtonsoft.Json;
using System.Collections.Generic;

namespace MetalApi.GuandlModel
{
    internal class GuandlMetalModel
    {
        //names comes from external api json structure
        internal const string NAME = "name";
        internal const string NEWEST_AVAILALE_DATE = "newest_available_date";
        internal const string OLDEST_AVAILABLE_DATE = "oldest_available_date";
        internal const string DATA = "data";

        [JsonProperty(NAME)]
        internal string Name { get; set; }

        [JsonProperty(NEWEST_AVAILALE_DATE)]
        internal string NewestAvailaleDate { get; set; }

        [JsonProperty(OLDEST_AVAILABLE_DATE)]
        internal string OldestAvailableDate { get; set; }

        [JsonProperty(DATA)]
        internal List<List<object>> Data { get; set; }
    }
}
