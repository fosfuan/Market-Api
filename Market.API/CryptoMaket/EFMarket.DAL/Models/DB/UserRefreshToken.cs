using System;
using System.Collections.Generic;

namespace CryptoMaket.EFMarket_DAL.Models.DB
{
    public partial class UserRefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }

        public User User { get; set; }
    }
}
