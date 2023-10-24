using AccountantWPF.Data;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Tests
{
    public abstract class TestBase
    {
        public async Task<AccountantDbContext> GetDbContext(bool useNewDb)
        {
            var connection = new SqliteConnection($"DataSource=:memory:");
            var builder = new DbContextOptionsBuilder<AccountantDbContext>();
            builder.UseSqlite(connection, x => { });

            var dbContext = new AccountantDbContext(builder.Options);
            if (useNewDb)
            {
                await dbContext.Database.EnsureDeletedAsync();
            }

            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.EnsureCreatedAsync();
            return dbContext;
        }
    }
}
