using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Market.DAL;
using Market.DAL.Repositories;

namespace Market.Services.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICoinsRepository coinsRepository;

        public CoinService(ICoinsRepository coinsRepository)
        {
            this.coinsRepository = coinsRepository;
        }
        public async Task<IList<CryptoCoinsHistory>> TakeAndSkipLatestCoinsValue(int skip, int take)
        {
            var latestCoinsValue = await this.coinsRepository.TakeAndSkipLatestCoinsValue(skip, take);

            return latestCoinsValue;
        }
    }
}
