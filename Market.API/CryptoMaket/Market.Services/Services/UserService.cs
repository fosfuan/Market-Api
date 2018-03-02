using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Market.DAL;
using Market.DAL.Repositories;
using Market.Helper;

namespace Market.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;

        public UserService(IUserRepository userRepository,
            IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
            this.userRepository = userRepository;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            var insertedUserId =  await this.userRepository.AddUserAsync(user);
            var isRoleAssignedToNewUser = await this.userRoleRepository.AddUserRole(insertedUserId, RoleType.BasicUser);

            return isRoleAssignedToNewUser;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            return await this.userRepository.CheckIfEmailExists(email);
        }

        public async Task<bool> CheckIfUsernameExists(string username)
        {
            return await this.userRepository.CheckIfUsernameExists(username);
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
