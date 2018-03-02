using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<User> GetUserByUsernamePassword(string userName, string password);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> CheckIfUsernameExists(string username);
        Task<bool> CheckIfEmailExists(string email);
    }
}
