using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;
using EFMarket.DAL;
using Market.DAL;
using Market.DAL.Repositories;

namespace Market.Services.Services
{
    public class CoinService : ICoinService
    {
        private IUnitOfWork unitOfWork;

        public CoinService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IList<CryptoCoinsHistory> TakeAndSkipLatestCoinsValue(int skip, int take)
        {
            return this.unitOfWork.CoinsRepository.TakeAndSkipLatestCoinsValue(skip, take);
        }
    }
}
