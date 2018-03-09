using CryptoMaket.Models;
using Market.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Managers
{
    public interface IUserManager
    {
        Task<User> Authenticate(LoginModel login);
        Task<LoginResponseData> BuildToken(User user);
        Task<bool> ValidateEmail(string email);
        Task<bool> ValidateUserName(string userName);
        Task<LoginResponseData> RefreshToken(string refreshToken, int userId);
    }
}
