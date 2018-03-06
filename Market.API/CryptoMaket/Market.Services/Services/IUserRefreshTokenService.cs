using Market.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public interface IUserRefreshTokenService
    {
        Task<bool> AddRefreshToken(int userId, string refreshToken);
        Task<bool> RemoveRefreshToken(int userId);
        Task<UserRefreshToken> GetRefreshTokenByUserId(int userId);
    }
}
