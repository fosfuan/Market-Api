using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Models
{
    public class LoginResponseData
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string userName { get; set; }
        public int userId { get; set; }

    }
}
