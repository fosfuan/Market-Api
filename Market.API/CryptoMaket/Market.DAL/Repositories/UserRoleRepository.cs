using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Market.DAL.Queries;
using Market.Helper;

namespace Market.DAL.Repositories
{
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        public UserRoleRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> AddUserRole(int userId, string role)
        {
            var isAddedUserRole = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(UserRolesQueries.InsertUserRole(), cn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = role;

                    await cn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    cn.Close();
                }
                isAddedUserRole = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return isAddedUserRole;
        }
    }
}
