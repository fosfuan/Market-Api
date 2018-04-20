using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFMarket.DAL.EFRepositories
{
    public interface IUserRoleRepository
    {
        Task<bool> AddUserRole(int userId, string role);
        Task<List<string>> GetUserRoles(int userId);
    }
}
