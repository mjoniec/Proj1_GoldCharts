using CommonModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public class FallbackMetalsPricesProvider : IMetalProvider
    {
        public async Task<string> Get(Metal metalType)
        {
            string json;
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
                + "//MetalPricesFallbackData//";

            if (metalType == Metal.Gold)
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
