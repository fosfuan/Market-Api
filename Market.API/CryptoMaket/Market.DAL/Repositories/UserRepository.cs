using Market.DAL.Queries;
using Market.Helper;
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
        public UserRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<bool> AddUserAsync(User user)
        {
            Guid userGuid = System.Guid.NewGuid();
            user.UserGuid = userGuid;

            string hashedPassword = Security.HashSHA1(user.Password + userGuid.ToString());

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(UserQueries.InsertUser(), cn))
            {
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = user.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = hashedPassword;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = user.UserName;
                cmd.Parameters.Add("@UserGuid", SqlDbType.UniqueIdentifier).Value = user.UserGuid;

                cn.Open();
                await cmd.ExecuteNonQueryAsync();
                cn.Close();
            }

            return true;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            bool userExists = false;

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
                    if(numberOfUsers > 0)
                    {
                        userExists = true;
                    }
                }
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

            return userExists;
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsernamePassword(string userName, string password)
        {
            var searchedUser = new User();
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(UserQueries.SelectUserByUserName(), con);
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = userName;

                await con.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    User user = new User();
                    user.Email = rdr["Email"].ToString();
                    user.Id = Convert.ToInt32(rdr["Id"]);
                    Guid userGuid;
                    Guid.TryParse(rdr["UserGuid"].ToString(), out userGuid);

                    user.UserName = rdr["UserName"].ToString();
                    var userPassword = rdr["Password"].ToString();

                    string hashedPassword = Security.HashSHA1(userPassword + userGuid);
                    if (string.Equals(hashedPassword, user.Password))
                    {
                        searchedUser = user;
                    }
                }
                con.Close();
            }

            return searchedUser;
        }



        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
