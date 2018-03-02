using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DAL.Queries
{
    public static class UserRolesQueries
    {
        public static string InsertUserRole()
        {
            return $"INSERT INTO [dbo].[UserRoles] (UserId, RoleName) VALUES(@UserId, @RoleName)";
        }

        public static string SelectUserRoleByUserId()
        {
            return $"SELECT RoleName FROM [dbo].[UserRoles] WHERE UserId = @UserId";
        }
    }
}
