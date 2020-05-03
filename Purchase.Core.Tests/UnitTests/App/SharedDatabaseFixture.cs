using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Purchase.Core.Tests.UnitTests.App
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;
        public DbConnection Connection { get; }

        public SharedDatabaseFixture()
        {
            Connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=PurchaseDatabase;Trusted_Connection=True");
            //Seed();
            Connection.Open();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
