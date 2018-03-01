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
        Task<User> GetUserByUsernameAndPassword(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
    }
}
