using AccountantWPF.Data.Models;
using AccountantWPF.Features.CashPosIncomes;
using AccountantWPF.Incomes;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Tests
{
    public class CashPosServiceTests : TestBase
    {
        [Fact]
        public async Task Add_Correct_CashPos_ToDataBase()
        {
            // Arrange
            var dbContext = await GetDbContext(true);

            var income = new Income
            {
                Name = "CashPos",
                CreatedOn = DateTime.Now.Date,
            };

            var incomeService = new IncomeService(dbContext);
            var cashPosService = new CashPosService(dbContext);
            await incomeService.CreateAsync(income);

            var cashPos1 = new CashPos
            {
                Name = "Autoban",
                IncomeId = income.Id,
                CreatedOn = DateTime.Now.Date,
            };

            var cashPos2 = new CashPos
            {
                Name = "Autoban",
                IncomeId = income.Id,
                CreatedOn = DateTime.Now.Date,
            };

            var cashPos3 = new CashPos
            {
                Name = "Bar",
                IncomeId = income.Id,
                CreatedOn = DateTime.Now.Date,
            };

            // Act
            var isCreated1 = await cashPosService.CreateAsync(cashPos1);
            var isCreated2 = await cashPosService.CreateAsync(cashPos2);
            var isCreated3 = await cashPosService.CreateAsync(cashPos3);
            var cashPos1ByDb = await dbContext.CashPosIncomes.Where(x => x.Name == "Autoban").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);
            var cashPos3ByDb = await dbContext.CashPosIncomes.Where(x => x.Name == "Bar").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);

            // Assert
            Assert.NotNull(cashPos1ByDb);
            Assert.NotNull(cashPos3ByDb);
            Assert.Equal("Autoban", cashPos1ByDb.Name);
            Assert.True(isCreated1);
            Assert.True(isCreated3);
            Assert.False(isCreated2);
            Assert.Equal(0, cashPos1ByDb.Pos);
            Assert.Equal(0, cashPos1ByDb.Cash);
            Assert.Empty(cashPos1ByDb.CashRegisters);
            Assert.Equal(0, cashPos3ByDb.Cash);
            Assert.Equal(0, cashPos3ByDb.Pos);
        }
    }
}
