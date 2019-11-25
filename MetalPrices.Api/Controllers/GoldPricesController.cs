using System.Collections.Generic;
using MetalPrices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalPrices.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoldPricesController : ControllerBase
    {
        IGoldPricesClient _goldPricesClient;

        public GoldPricesController(IGoldPricesClient goldPricesClient)
        {
            _goldPricesClient = goldPricesClient;
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
            _goldPricesClient.StartDownloadingDailyGoldPrices();

            return Accepted();
        }

        // GET: GoldPrices/Daily
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Daily()
        {
            var dailyGoldPrices = _goldPricesClient.DailyGoldPrices;

            if (string.IsNullOrEmpty(dailyGoldPrices))
            {
                return NoContent();
            }

            return Ok(dailyGoldPrices);
        }
    }
}
