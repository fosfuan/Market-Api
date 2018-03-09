using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Market.DAL.Queries;
using Market.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Market.DAL.Repositories
{
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        private readonly ILogger<UserRoleRepository> logger;

        public UserRoleRepository(IConfiguration config,
            ILogger<UserRoleRepository> logger) : base(config.GetConnectionString("DefaultConnection"))
        {
            this.logger = logger;
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
                this.logger.LogError(ex.Message);
                throw ex;
            }

            return isAddedUserRole;
        }

        public async Task<List<string>> GetUserRoles(int userId)
        {
            List<string> userRoles = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(UserRolesQueries.SelectUserRoleByUserId(), con);
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    await con.OpenAsync();

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        string role = reader["RoleName"].ToString();
                        userRoles.Add(role);
                    }
                }
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }

            return userRoles;
        }
    }
}
