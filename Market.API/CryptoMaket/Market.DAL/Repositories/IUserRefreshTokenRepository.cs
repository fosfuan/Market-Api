using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public interface IUserRefreshTokenRepository
    {
        Task<bool> AddRefreshToken(int userId, string refreshToken);
        Task<bool> RemoveRefreshToken(int userId);
        Task<UserRefreshToken> GetRefreshTokenByUserId(int userId);
    }
}
