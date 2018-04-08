using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public interface ICoinsRepository
    {
        Task<IList<CryptoCoinsHistory>> TakeAndSkipLatestCoinsValue(int skip, int take);
    }
}
