using CryptoMaket.EFMarket_DAL.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public class UserRoleRepository : BaseRepository<UserRoles>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context)
        {
        }
        public async Task<bool> AddUserRole(int userId, string role)
        {
            UserRoles userRoles = new UserRoles();
            userRoles.RoleName = role;
            userRoles.UserId = userId;
            try
            {
                await this.CreateAsync(userRoles);
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetUserRoles(int userId)
        {
            var userRoles = await this.dbSet.Where(x => x.UserId == userId).Select(x=>x.RoleName).ToListAsync();
            return userRoles;
        }
    }
}
