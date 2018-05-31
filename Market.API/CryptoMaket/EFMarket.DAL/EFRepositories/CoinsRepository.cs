using CryptoMaket.EFMarket_DAL.Models.DB;
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
        public async Task<IList<CryptoCoinsHistory>> TakeAndSkipLatestCoinsValue(int skip, int take)
        {
            var groupedByName = this.dbSet.GroupBy(x => x.CoinId).SelectMany(a => a.Where(b => b.Id == a.Max(c => c.Id))).OrderBy(coin => coin.CoinId).Skip(skip).Take(take);

            return await groupedByName.ToListAsync();
        }

        public async Task<IList<CryptoCoinsHistory>> TakeSpecificCurrencyHistory(string currencyName)
        {
            var historyForSpecificCurrencySelectedById = this.dbSet.Where(coin => coin.Symbol.Equals(currencyName)).OrderByDescending(coin => coin.UpdatedTime);

            return await historyForSpecificCurrencySelectedById.ToListAsync();
        }
    }
}
