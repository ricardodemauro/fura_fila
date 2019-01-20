using FuraFila.Core.Data;
using FuraFila.Data.SQlite.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Data.SQlite
{
    public class SQliteDatabase : IDatabase
    {
        private readonly string _connectionString;

        public SQliteDatabase(SQliteConnectionOptions opts)
        {
            _connectionString = opts.ConnectionString;
        }

        public IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}
