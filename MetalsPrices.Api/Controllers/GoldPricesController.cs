using System.Collections.Generic;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "GoldPricesController is working" };
        }

        // GET: GoldPrices/StartDownloadingDaily
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult StartDownloadingDaily()
        {
            _metalPricesService.StartPreparingPrices();

            return Accepted();
        }

        // GET: GoldPrices/Daily
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Daily()
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
