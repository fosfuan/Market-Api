using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;

namespace EFMarket.DAL.EFRepositories
{
    public interface ICoinsRepository
    {
        IList<CryptoCoinsHistory> TakeAndSkipLatestCoinsValue(int skip, int take);
    }
}
