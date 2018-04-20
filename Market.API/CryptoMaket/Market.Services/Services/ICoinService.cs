﻿using CryptoMaket.EFMarket_DAL.Models.DB;
using Market.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public interface ICoinService
    {
        IList<CryptoCoinsHistory> TakeAndSkipLatestCoinsValue(int skip, int take);
    }
}
