using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Market.DAL;
using Market.DAL.Repositories;

namespace Market.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            return await this.userRepository.AddUserAsync(user);
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsernameAndPassword(User user)
        {
            return await this.userRepository.GetUserByUsernamePassword(user.UserName, user.Password);
        }

        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
