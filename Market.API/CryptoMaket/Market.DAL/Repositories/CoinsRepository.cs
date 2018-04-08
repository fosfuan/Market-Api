using Market.DAL.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public class CoinsRepository : BaseRepository, ICoinsRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<CoinsRepository> logger;

        public CoinsRepository(IConfiguration config,
            ILogger<CoinsRepository> logger) : base(config.GetConnectionString("DefaultConnection"))
        {
            this.logger = logger;
        }

        public async Task<IList<CryptoCoinsHistory>> TakeAndSkipLatestCoinsValue(int skip, int take)
        {
            IList<CryptoCoinsHistory> latestCoinsValue = new List<CryptoCoinsHistory>();
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(CoinQueries.GetLatestValuesForPassedNumberOfCoins(), con);
                cmd.Parameters.Add("@Skip", SqlDbType.Int).Value = skip;
                cmd.Parameters.Add("@Take", SqlDbType.Int).Value = take;

                await con.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    CryptoCoinsHistory latestCoinValue = new CryptoCoinsHistory();
                    int coinId = Convert.ToInt32(rdr["CoinId"]);
                    if (coinId != 0)
                    {
                        latestCoinValue.CoinId = coinId;
                        latestCoinValue.Name = rdr["Name"].ToString();
                        latestCoinValue.Symbol = rdr["Symbol"].ToString();
                        latestCoinValue.Price_BTC = Decimal.Parse(rdr["Price_BTC"].ToString());
                        latestCoinValue.Price_USD = Decimal.Parse(rdr["Price_USD"].ToString());
                        latestCoinValue.Change_1H = Decimal.Parse(rdr["Change_1H"].ToString());
                        latestCoinValue.Change_24H = Decimal.Parse(rdr["Change_24H"].ToString());
                        latestCoinValue.Change_1W = Decimal.Parse(rdr["Change_1W"].ToString());
                        latestCoinValue.Updated_Time = Convert.ToDateTime(rdr["Updated_Time"]);

                        latestCoinsValue.Add(latestCoinValue);
                    }
                }
                con.Close();
            }

            return latestCoinsValue;
        }
    }
}
