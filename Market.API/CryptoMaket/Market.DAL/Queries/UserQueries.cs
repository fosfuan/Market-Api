using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DAL.Queries
{
    public static class UserQueries
    {
        public static string SelectAllUsers()
        {
            return $"SELECT * FROM [dbo].[User]";
        }

        public static string SelectUserByUserName()
        {
            return $"SELECT * FROM [dbo].[User] WHERE UserName = @UserName";
        }

        public static string InsertUser()
        {
            return $"INSERT INTO [dbo].[User] (Email, Password, UserName, UserGuid) VALUES(@Email, @Password, @UserName, @UserGuid)";
        }

    }
}
