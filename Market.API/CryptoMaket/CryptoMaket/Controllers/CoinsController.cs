using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoMaket.Models;
using Market.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CryptoMaket.Controllers
{
    [Produces("application/json")]
    [Route("api/coins")]
    public class CoinsController : Controller
    {
        private IConfiguration config;
        private ICoinService coinService;

        public CoinsController(ICoinService coinService)
        {
            this.coinService = coinService;
        }


        [AllowAnonymous]
        [HttpPost("latest/values")]
        public async Task<IActionResult> GetLastCoinsValues([FromBody]SkipTakeModel skipTake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ValidateTake(ref skipTake, 10);
            try
            {
                var getLatestCoinsValue = await this.coinService.TakeAndSkipLatestCoinsValue(skipTake.Skip, skipTake.Take);

                return Ok(getLatestCoinsValue);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("history/{id}")]
        public  async Task<IActionResult> GetCoinHistory(int id)
        {
            try
            {
                var coinHistory = await this.coinService.TakeSpecificCurrencyHistory(id);
                if(coinHistory.Count > 0)
                    return Ok(coinHistory);

                return BadRequest("Not foud item with specific id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        private void ValidateTake(ref SkipTakeModel model, int howManyTake)
        {
            if (model.Take == 0)
                model.Take = howManyTake;
        }


    }
}