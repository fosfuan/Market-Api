using System;
using System.Collections.Generic;

namespace CryptoMaket.EFMarket_DAL.Models.DB
{
    public partial class CrytpoCoins
    {
        public CrytpoCoins()
        {
            CryptoCoinsHistory = new HashSet<CryptoCoinsHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public ICollection<CryptoCoinsHistory> CryptoCoinsHistory { get; set; }
    }
}
