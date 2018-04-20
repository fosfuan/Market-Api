using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    interface IBaseRepository<T>
    {
        T Get(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
