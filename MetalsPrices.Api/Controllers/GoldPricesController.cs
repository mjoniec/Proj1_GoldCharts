using MetalsPrices.Abstraction.MeralPricesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalsPrices.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoldPricesController : ControllerBase
    {
        IMetalPricesService _metalPricesService;

        public GoldPricesController(IMetalPricesService metalPricesService)
        {
            _metalPricesService = metalPricesService;
        }

        // GET: GoldPrices
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok("GoldPricesController is working");
        }

        // GET: GoldPrices/StartPreparingPrices
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult StartPreparingPrices()
        {
            _metalPricesService.StartPreparingPrices();

            return Accepted("Started preparing gold prices");
        }

        // GET: GoldPrices/Prices
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Prices()
        {
            var dailyGoldPrices = _metalPricesService.GetPrices();

            if (dailyGoldPrices == null)
            {
                return NoContent();
            }

            return Ok(dailyGoldPrices);
        }
    }
}
