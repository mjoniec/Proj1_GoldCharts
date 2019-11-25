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
        //https://localhost:44359/GoldPrices
        // GET: 
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "GoldPricesController is working" };
        }

        // GET: 
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetDaily()
        {
            var dailyGoldPrices = _goldPricesClient.GetDailyGoldPrices();

            return Ok(dailyGoldPrices);
        }
    }
}
