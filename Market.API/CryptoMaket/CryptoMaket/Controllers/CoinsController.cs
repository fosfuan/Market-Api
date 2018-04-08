﻿using System;
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
        public async Task<IActionResult> CreateToken([FromBody]SkipTakeModel skipTake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ValidateTake(ref skipTake);
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

        private void ValidateTake(ref SkipTakeModel model)
        {
            if (model.Take == 0)
                model.Take = 10;
        }


    }
}