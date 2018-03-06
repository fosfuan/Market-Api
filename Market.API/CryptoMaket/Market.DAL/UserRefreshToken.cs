using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DAL
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
