using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DAL.Queries
{
    public static class CoinQueries
    {
        public static string GetLatestValuesForPassedNumberOfCoins()
        {
            return @"
            SELECT cch.CoinId, cch.Name, cch.Symbol,cch.Price_BTC,cch.Price_USD, cch.Change_1H, cch.Change_24H, cch.Change_1W,cch.Updated_Time
                FROM dbo.CryptoCoinsHistory
                AS cch
            INNER JOIN dbo.CrytpoCoins AS cc ON cch.CoinId = cc.Id
            WHERE cch.Id in (
	            SELECT max(Id)
	            FROM dbo.CryptoCoinsHistory
	             GROUP BY CoinId
            )
            ORDER BY cc.Id 
            OFFSET     @Skip ROWS      
            FETCH NEXT @Take ROWS ONLY";
        }
    }
}
