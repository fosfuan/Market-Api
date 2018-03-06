using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DAL.Queries
{
    public class RefreshTokenQueries
    {
        public static string InserRefreshToken()
        {
            return $"INSERT INTO [dbo].[UserRefreshToken] (UserId, RefreshToken) VALUES(@UserId, @RefreshToken)";
        }

        public static string DeleteRefreshToken()
        {
            return $"DELETE FROM [dbo].[UserRefreshToken] WHERE UserId = @UserId";
        }

        public static string SelectRefreshTokenByUserId()
        {
            return $"SELECT * FROM [dbo].[UserRefreshToken] WHERE UserId = @UserId";
        }
    }
}
