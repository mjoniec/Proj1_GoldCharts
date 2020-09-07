using Microsoft.AspNetCore.Mvc;
using MetalsDataProvider.Providers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GoldChartsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        IMetalsPricesProvider _metalsPricesProvider;

        public GoldController(IMetalsPricesProvider metalsPricesProvider)
        {
            _metalsPricesProvider = metalsPricesProvider;
        }

        // GET: GoldPrices
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok("GoldPricesController is working");
        }

        // GET: GoldPrices/Prices
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPrices()
        {
            var dailyGoldPrices = await _metalsPricesProvider.GetGoldPrices();

            if (dailyGoldPrices == null)
            {
                return NoContent();
            }

            return Ok(dailyGoldPrices);
        }
    }
}
