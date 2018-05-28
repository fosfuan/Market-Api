using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;

namespace EFMarket.DAL.EFRepositories
{
    public interface ICoinsRepository
    {
        Task<IList<CryptoCoinsHistory>> TakeAndSkipLatestCoinsValue(int skip, int take);
        Task<IList<CryptoCoinsHistory>> TakeSpecificCurrencyHistory(int id);
    }
}
