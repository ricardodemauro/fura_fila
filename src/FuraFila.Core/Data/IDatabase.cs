using System;
using System.Data;

namespace FuraFila.Core.Data
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();
    }
}
