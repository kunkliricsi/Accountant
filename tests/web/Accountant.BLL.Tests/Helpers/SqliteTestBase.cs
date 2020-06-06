using Accountant.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace Accountant.BLL.Tests.Helpers
{
    public abstract class SqliteTestBase : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly AccountantContext DbContext;

        protected SqliteTestBase()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<AccountantContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new AccountantContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
