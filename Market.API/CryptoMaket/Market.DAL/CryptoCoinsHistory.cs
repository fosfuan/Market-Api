using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DAL
{
    public class CryptoCoinsHistory
    {
        public int Id { get; set; }
        public int CoinId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Price_USD { get; set; }
        public decimal Price_BTC { get; set; }
        public decimal Volume_24_USD { get; set; }
        public decimal Change_1H { get; set; }
        public decimal Change_24H { get; set; }
        public decimal Change_1W { get; set; }
        public DateTime Updated_Time { get; set; }
    }
}
