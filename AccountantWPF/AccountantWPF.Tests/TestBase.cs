using AccountantWPF.Data;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Tests
{
    public abstract class TestBase
    {
        public async Task<AccountantDbContext> GetDbContext(bool isNewDb)
        {
            var connection = new SqliteConnection($"DataSource=:memory:");
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite(connection, x => { });

            var options = new DbContextOptionsBuilder<AccountantDbContext>().Options;

            var dbContext = new AccountantDbContext(builder.Options);
            if (isNewDb)
            {
                await dbContext.Database.EnsureDeletedAsync();
            }

            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.EnsureCreatedAsync();
            return dbContext;
        }
    }
}
