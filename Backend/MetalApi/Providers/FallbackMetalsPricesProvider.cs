using Model;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public class FallbackMetalsPricesProvider : IMetalProvider
    {
        public async Task<string> Get(MetalType metalType)
        {
            string json;
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
                + "//MetalPricesFallbackData//";

            if (metalType == MetalType.Gold)
            {
                path += "goldPricesFallback.json";
            }
            else
            {
                path += "silverPricesFallback.json";
            }

            json = await File.ReadAllTextAsync(path);

            return json;
        }
    }
}
