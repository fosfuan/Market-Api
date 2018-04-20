﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;
using EFMarket.DAL;
using EFMarket.DAL.EFRepositories;
using Market.DAL;
using Market.DAL.Repositories;

namespace Market.Services.Services
{
    public class CoinService : ICoinService
    {
        private ICoinsRepository coinRepository;

        public CoinService(IUnitOfWork unitOfWork)
        {
            this.coinRepository = unitOfWork.CoinsRepository;
        }
        public IList<CryptoCoinsHistory> TakeAndSkipLatestCoinsValue(int skip, int take)
        {
            return this.coinRepository.TakeAndSkipLatestCoinsValue(skip, take);
        }
    }
}
