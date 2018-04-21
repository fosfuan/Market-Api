using CryptoMaket.EFMarket_DAL.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public class UserRefreshTokenRepository : BaseRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(DbContext context) : base(context) { }
        public async Task<bool> AddRefreshToken(int userId, string refreshToken)
        {
            UserRefreshToken newRefreshToken = new UserRefreshToken();
            newRefreshToken.UserId = userId;
            newRefreshToken.RefreshToken = refreshToken;
            try
            {
                await this.CreateAsync(newRefreshToken);
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserRefreshToken> GetRefreshTokenByUserId(int userId)
        {
            return await this.dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<bool> RemoveRefreshToken(int userId)
        {
            try
            {
                var userRefreshToken = await this.GetRefreshTokenByUserId(userId);
                this.Delete(userRefreshToken);
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
                                
        }
    }
}
