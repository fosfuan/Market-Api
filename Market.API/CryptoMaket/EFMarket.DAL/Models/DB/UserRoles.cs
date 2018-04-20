using System;
using System.Collections.Generic;

namespace CryptoMaket.EFMarket_DAL.Models.DB
{
    public partial class UserRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoleName { get; set; }

        public User User { get; set; }
    }
}
