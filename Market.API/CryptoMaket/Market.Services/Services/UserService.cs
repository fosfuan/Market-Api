using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CryptoMaket.EFMarket_DAL.Models.DB;
using EFMarket.DAL;
using EFMarket.DAL.EFRepositories;
using Market.DAL;
using Market.DAL.Repositories;
using Market.Helper;

namespace Market.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;
        

        public UserService(IUnitOfWork unitOfWork)
        {
            this.userRepository = unitOfWork.UserRepository;
            this.userRoleRepository = unitOfWork.UserRoleRepository;
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

        public async Task DeleteUser(User user)
        {
            await this.userRepository.DeleteUser(user);
        }

        public async Task<User> GetUserByUsernameAndPassword(string userName, string password)
        {
            return await this.userRepository.GetUserByUsernamePassword(userName, password);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await this.userRepository.GetUserById(userId);
        }

        public async Task UpdateUser(User user)
        {
            await this.userRepository.UpdateUser(user);
        }
    }
}
