﻿using CryptoMaket.EFMarket_DAL.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public class CoinsRepository : BaseRepository<CryptoCoinsHistory>, ICoinsRepository
    {
        public CoinsRepository(DbContext dbContext) : base(dbContext)
        {

        }
        public IList<CryptoCoinsHistory> TakeAndSkipLatestCoinsValue(int skip, int take)
        {
            var groupedByName = this.dbSet.GroupBy(x => x.CoinId);
            var listOfLatestValues = groupedByName.SelectMany(a => a.Where(b => b.Id == a.Max(c => c.Id))).Skip(skip).Take(take);

            return listOfLatestValues.ToList();
        }
    }
}
