using EFMarket.DAL;
using EFMarket.DAL.EFRepositories;
using Market.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public class UserRolesService : IUserRolesService
    {
        private IUserRoleRepository userRoleRepository;

        public UserRolesService(IUnitOfWork unitOfWork)
        {
            this.userRoleRepository = unitOfWork.UserRoleRepository;
        }

        public async Task<bool> AddUserRole(int userId, string role)
        {
            return await this.userRoleRepository.AddUserRole(userId, role);
        }

        public async Task<List<string>> GetUserRoles(int userId)
        {
            return await this.userRoleRepository.GetUserRoles(userId);
        }
    }
}
