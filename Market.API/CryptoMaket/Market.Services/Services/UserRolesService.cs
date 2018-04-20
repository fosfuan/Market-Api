using EFMarket.DAL;
using Market.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public class UserRolesService : IUserRolesService
    {
        private IUnitOfWork unitOfWork;

        public UserRolesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddUserRole(int userId, string role)
        {
            return await this.unitOfWork.UserRoleRepository.AddUserRole(userId, role);
        }

        public async Task<List<string>> GetUserRoles(int userId)
        {
            return await this.unitOfWork.UserRoleRepository.GetUserRoles(userId);
        }
    }
}
