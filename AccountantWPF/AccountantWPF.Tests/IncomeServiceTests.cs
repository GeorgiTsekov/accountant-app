using AccountantWPF.Data.Models;
using AccountantWPF.Features.Incomes;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Tests
{
    public class IncomeServiceTests : TestBase
    {
        [Fact]
        public async Task Add_Correct_Income_ToDataBase()
        {
            // Arrange
            var dbContext = await GetDbContext(true);

            var income1 = new Income
            {
                Name = "Test",
                CreatedOn = DateTime.Now.Date,
            };

            var income2 = new Income
            {
                Name = "Test",
                CreatedOn = DateTime.Now.Date,
            };

            var income3 = new Income
            {
                Name = "CashPos",
                CreatedOn = DateTime.Now.Date,
            };

            // Act
            var incomeService = new IncomeService(dbContext);
            var isCreated1 = await incomeService.CreateAsync(income1);
            var isCreated2 = await incomeService.CreateAsync(income2);
            var isCreated3 = await incomeService.CreateAsync(income3);
            var income1ByDb = await dbContext.Incomes.Where(x => x.Name == "Test").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);
            var income3ByDb = await dbContext.Incomes.Where(x => x.Name == "CashPos").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);

            // Assert
            Assert.NotNull(income1ByDb);
            Assert.NotNull(income3ByDb);
            Assert.Equal("Test", income1ByDb.Name);
            Assert.True(isCreated1);
            Assert.True(isCreated3);
            Assert.False(isCreated2);
            Assert.Equal(0, income1ByDb.Pos);
            Assert.Equal(0, income1ByDb.Cash);
            Assert.Empty(income1ByDb.CashPosIncomes);
            Assert.Equal(0, income3ByDb.Cash);
            Assert.Equal(0, income3ByDb.Pos);
        }
    }
}
