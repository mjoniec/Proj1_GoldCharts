using System.Collections.Generic;
using MetalPrices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalPrices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldPricesController : ControllerBase
    {
        IGoldPricesClient _goldPricesClient;

        public GoldPricesController(IGoldPricesClient goldPricesClient)
        {
            _goldPricesClient = goldPricesClient;
        }

        // GET: api/GoldPrices
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "GoldPricesController is working" };
        }

        // GET: api/GoldPrices/5
        [HttpGet(Name = "GetDaily")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetDaily()
        {
            var dailyGoldPrices = _goldPricesClient.GetDailyGoldPrices();

            return Ok(dailyGoldPrices);
        }
    }
}
