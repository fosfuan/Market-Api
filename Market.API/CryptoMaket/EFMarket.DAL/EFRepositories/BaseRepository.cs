using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected DbContext dbContext;
        protected DbSet<T> dbSet;

        public BaseRepository(DbContext context)
        {
            this.dbContext = context;
            this.dbSet = dbContext.Set<T>();
        }

        public T Get(Func<T, bool> predicate)
        {
            var entity = dbSet.FirstOrDefault(predicate);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await dbSet.ToListAsync();

            return entities;
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

    }
}
