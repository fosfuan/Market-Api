using Market.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public interface IUserRoleRepository
    {
        Task<bool> AddUserRole(int userId, string role);
    }
}
