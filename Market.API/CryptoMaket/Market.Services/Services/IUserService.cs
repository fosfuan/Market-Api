using CryptoMaket.EFMarket_DAL.Models.DB;
using Market.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(User user);
        Task<User> GetUserByUsernameAndPassword(string userName, string password);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
        Task<bool> CheckIfUsernameExists(string username);
        Task<bool> CheckIfEmailExists(string email);
        Task<User> GetUserById(int userId);
    }
}
