using CryptoMaket.EFMarket_DAL.Models.DB;
using Market.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {

        }
        public async Task<int> AddUserAsync(User user)
        {
            try
            {
                Guid userGuid = System.Guid.NewGuid();
                user.UserGuid = userGuid;
                string hashedPassword = Security.HashSHA1(user.Password + userGuid.ToString());
                user.Password = hashedPassword;
                await this.CreateAsync(user);
                await this.dbContext.SaveChangesAsync();

                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            var user = await this.dbSet.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckIfUsernameExists(string username)
        {
            var user = await this.dbSet.FirstOrDefaultAsync(x => x.UserName == username);
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public async Task DeleteUser(User user)
        {
            this.Delete(user);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await this.dbSet.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByUsernamePassword(string userName, string password)
        {
            var user = await this.dbSet.FirstOrDefaultAsync(x => x.UserName == userName);
            Guid userGuid = user.UserGuid;
            var userPasswordFromDb = user.Password;
            string hashedPassword = Security.HashSHA1(password + userGuid);

            if (String.Equals(hashedPassword, userPasswordFromDb))
            {
                return user;
            }

            return null;
        }

        public async Task UpdateUser(User user)
        {
            this.Update(user);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
