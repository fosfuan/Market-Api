using CryptoMaket.EFMarket_DAL.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFMarket.DAL.EFRepositories
{
    public class EFUserRepository : BaseRepository<User>
    {
        public EFUserRepository(DbContext context) : base(context)
        {
        }

        public User GetUserById(int id)
        {
            return this.dbSet.FirstOrDefault(x => x.Id == id);
        }
    }
}
