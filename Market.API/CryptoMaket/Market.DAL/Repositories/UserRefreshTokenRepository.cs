//using Market.DAL.Queries;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Text;
//using System.Threading.Tasks;

//namespace Market.DAL.Repositories
//{
//    public class UserRefreshTokenRepository : BaseRepository, IUserRefreshTokenRepository
//    {
//        private readonly ILogger<UserRefreshTokenRepository> logger;

//        public UserRefreshTokenRepository(IConfiguration config,
//            ILogger<UserRefreshTokenRepository> logger) : base(config.GetConnectionString("DefaultConnection"))
//        {
//            this.logger = logger;
//        }

//        public async Task<bool> AddRefreshToken(int userId, string refreshToken)
//        {
//            bool isTokenAdded = false;
//            try
//            {
//                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
//                using (SqlCommand cmd = new SqlCommand(RefreshTokenQueries.InserRefreshToken(), cn))
//                {
//                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
//                    cmd.Parameters.Add("@RefreshToken", SqlDbType.NVarChar, 250).Value = refreshToken;
//                    await cn.OpenAsync();
//                    await cmd.ExecuteNonQueryAsync();
//                    cn.Close();
//                }
//                isTokenAdded = true;
//            }
//            catch (Exception ex)
//            {
//                this.logger.LogError(ex.Message);
//                throw ex;
//            }
//            return isTokenAdded;
//        }

//        public async Task<UserRefreshToken> GetRefreshTokenByUserId(int userId)
//        {
//            UserRefreshToken userRefreshToken = new UserRefreshToken();
//            try
//            {
//                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
//                using (SqlCommand cmd = new SqlCommand(RefreshTokenQueries.SelectRefreshTokenByUserId(), cn))
//                {
//                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
//                    await cn.OpenAsync();

//                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

//                    while (await reader.ReadAsync())
//                    {
//                        userRefreshToken.Id = Convert.ToInt32(reader["Id"]);
//                        userRefreshToken.UserId = Convert.ToInt32(reader["UserId"]);
//                        userRefreshToken.RefreshToken = reader["RefreshToken"].ToString(); 
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                this.logger.LogError(ex.Message);
//                throw ex;
//            }
//            if (string.IsNullOrWhiteSpace(userRefreshToken.RefreshToken))
//                return null;

//            return userRefreshToken;
//        }

//        public async Task<bool> RemoveRefreshToken(int userId)
//        {
//            bool isTokenRemoved = false;
//            try
//            {
//                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
//                using (SqlCommand cmd = new SqlCommand(RefreshTokenQueries.DeleteRefreshToken(), cn))
//                {
//                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
//                    await cn.OpenAsync();
//                    await cmd.ExecuteNonQueryAsync();
//                    cn.Close();
//                }
//                isTokenRemoved = true;
//            }
//            catch (Exception ex)
//            {
//                this.logger.LogError(ex.Message);
//                throw ex;
//            }
//            return isTokenRemoved;
//        }
//    }
//}
