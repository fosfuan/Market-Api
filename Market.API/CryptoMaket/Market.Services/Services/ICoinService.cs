﻿using Market.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public interface ICoinService
    {
        Task<IList<CryptoCoinsHistory>> TakeAndSkipLatestCoinsValue(int skip, int take);
    }
}
