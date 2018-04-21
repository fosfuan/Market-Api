using CryptoMaket.EFMarket_DAL.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public interface IUserRefreshTokenRepository
    {
        Task<bool> AddRefreshToken(int userId, string refreshToken);
        Task<bool> RemoveRefreshToken(int userId);
        Task<UserRefreshToken> GetRefreshTokenByUserId(int userId);
    }
}
