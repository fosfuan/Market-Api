using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;
using EFMarket.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMaket.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ValuesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET api/values
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IList<CryptoCoinsHistory>> Get()
        {
            var lastMaxValues = await this.unitOfWork.CoinsRepository.TakeAndSkipLatestCoinsValue(0, 10);
            return lastMaxValues;
        }

        // GET api/values/5
        [Authorize(Roles = "BasicUser")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
