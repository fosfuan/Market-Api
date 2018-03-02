using Market.DAL.Queries;
using Market.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(IConfiguration config,
            ILogger<UserRepository> logger) : base(config.GetConnectionString("DefaultConnection"))
        {
            this.logger = logger;
        }

        public async Task<int> AddUserAsync(User user)
        {
            Guid userGuid = System.Guid.NewGuid();
            user.UserGuid = userGuid;
            int insertedId = 0;

            string hashedPassword = Security.HashSHA1(user.Password + userGuid.ToString());
            try
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(UserQueries.InsertUser(), cn))
                {
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = user.Email;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = hashedPassword;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = user.UserName;
                    cmd.Parameters.Add("@UserGuid", SqlDbType.UniqueIdentifier).Value = user.UserGuid;

                    cn.Open();
                    insertedId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    //await cmd.ExecuteNonQueryAsync();
                    if (cn.State == System.Data.ConnectionState.Open) cn.Close();
                }
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }

            return insertedId;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            bool userExists = false;
            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(UserQueries.CountUserByEmail(), con);
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = email;
                    await con.OpenAsync();

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        int numberOfUsers;
                        GetNumberOfUsers(out numberOfUsers, reader);
                        if (numberOfUsers > 0)
                        {
                            userExists = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }

            return userExists;
        }

        private void GetNumberOfUsers(out int numberOfUsers, SqlDataReader reader)
        {
            numberOfUsers = Convert.ToInt32(reader["NumberOfUsers"]);
        }


        public async Task<bool> CheckIfUsernameExists(string username)
        {
            bool userExists = false;
            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(UserQueries.CountUserByUserName(), con);
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
                    await con.OpenAsync();

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        int numberOfUsers;
                        GetNumberOfUsers(out numberOfUsers, reader);
                        if (numberOfUsers > 0)
                        {
                            userExists = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }

            return userExists;
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsernamePassword(string userName, string password)
        {
            var searchedUser = new User();
            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(UserQueries.SelectUserByUserName(), con);
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName;

                    await con.OpenAsync();
                    SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                    while (await rdr.ReadAsync())
                    {
                        User user = new User();
                        Guid userGuid;
                        Guid.TryParse(rdr["UserGuid"].ToString(), out userGuid);

                        var userPassword = rdr["Password"].ToString();
                        string hashedPassword = Security.HashSHA1(password + userGuid);

                        if (string.Equals(hashedPassword, userPassword))
                        {
                            user.UserName = rdr["UserName"].ToString();
                            user.Email = rdr["Email"].ToString();
                            user.Id = Convert.ToInt32(rdr["Id"]);
                            user.UserGuid = userGuid;
                            searchedUser = user;
                        }
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
            return searchedUser;
        }



        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
