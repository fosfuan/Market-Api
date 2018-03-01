using System;
using System.Collections.Generic;
using System.Text;

namespace Market.DAL.Repositories
{
    public class BaseRepository
    {
        public string ConnectionString { get; set; }

        public BaseRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
