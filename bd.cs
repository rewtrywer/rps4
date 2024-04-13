using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using Microsoft.Data.Sqlite;


namespace rps4
{
    internal class BD
    {
        //string connectionString = @"Data Source = bd.db";
        string connectionString = @"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db";


        public string getConnectionString()
        {
            return connectionString;
        }

    }
}
