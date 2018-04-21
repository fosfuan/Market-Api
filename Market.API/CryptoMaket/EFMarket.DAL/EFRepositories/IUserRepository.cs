using CryptoMaket.EFMarket_DAL.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        Task<User> GetUserByUsernamePassword(string userName, string password);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
        Task<bool> CheckIfUsernameExists(string username);
        Task<bool> CheckIfEmailExists(string email);
        Task<User> GetUserById(int userId);
    }
}
