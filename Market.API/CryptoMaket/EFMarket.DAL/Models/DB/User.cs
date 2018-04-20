using System;
using System.Collections.Generic;

namespace CryptoMaket.EFMarket_DAL.Models.DB
{
    public partial class User
    {
        public User()
        {
            UserRefreshToken = new HashSet<UserRefreshToken>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Guid UserGuid { get; set; }

        public ICollection<UserRefreshToken> UserRefreshToken { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
