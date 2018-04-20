using System;
using System.Collections.Generic;

namespace CryptoMaket.EFMarket_DAL.Models.DB
{
    public partial class CryptoCoinsHistory
    {
        public int Id { get; set; }
        public int CoinId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal? PriceUsd { get; set; }
        public decimal? PriceBtc { get; set; }
        public decimal? Volume24Usd { get; set; }
        public decimal? Change1h { get; set; }
        public decimal? Change24h { get; set; }
        public decimal? Change1w { get; set; }
        public DateTime UpdatedTime { get; set; }

        public CrytpoCoins Coin { get; set; }
    }
}
