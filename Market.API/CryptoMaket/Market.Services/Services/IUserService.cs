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
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> CheckIfUsernameExists(string username);
        Task<bool> CheckIfEmailExists(string email);
    }
}
