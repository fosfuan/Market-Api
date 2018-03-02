using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public interface IUserRolesService
    {
        Task<bool> AddUserRole(int userId, string role);
        Task<List<string>> GetUserRoles(int userId);
    }
}
