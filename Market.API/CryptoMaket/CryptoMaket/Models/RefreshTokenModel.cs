﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Models
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
