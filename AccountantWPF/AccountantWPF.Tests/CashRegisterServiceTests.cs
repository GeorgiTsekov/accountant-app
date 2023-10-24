using AccountantWPF.Data.Models;
using AccountantWPF.Features.CashPosIncomes;
using AccountantWPF.Features.CashRegisters;
using AccountantWPF.Features.Incomes;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Tests
{
    public class CashRegisterServiceTests : TestBase
    {
        [Fact]
        public async Task Add_Correct_CashRegister_ToDataBase()
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
            var cashRegisterService = new CashRegisterService(dbContext);
            await incomeService.CreateAsync(income);

            var cashPos = new CashPos
            {
                Name = "Autoban",
                IncomeId = income.Id,
                CreatedOn = DateTime.Now.Date,
            };

            await cashPosService.CreateAsync(cashPos);

            var cashRegister1 = new CashRegister
            {
                Name = "CashRegister1",
                CashPosId = cashPos.Id,
                CreatedOn = DateTime.Now.Date,
            };

            var cashRegister2 = new CashRegister
            {
                Name = "CashRegister1",
                CashPosId = cashPos.Id,
                CreatedOn = DateTime.Now.Date,
            };

            var cashRegister3 = new CashRegister
            {
                Name = "CashRegister2",
                CashPosId = cashPos.Id,
                CreatedOn = DateTime.Now.Date,
            };

            // Act
            var isCreated1 = await cashRegisterService.CreateAsync(cashRegister1);
            var isCreated2 = await cashRegisterService.CreateAsync(cashRegister2);
            var isCreated3 = await cashRegisterService.CreateAsync(cashRegister3);
            var cashReg1ByDb = await dbContext.CashRegisters.Where(x => x.Name == "CashRegister1").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);
            var cashReg3ByDb = await dbContext.CashRegisters.Where(x => x.Name == "CashRegister2").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);

            // Assert
            Assert.NotNull(cashReg1ByDb);
            Assert.NotNull(cashReg3ByDb);
            Assert.Equal("CashRegister1", cashReg1ByDb.Name);
            Assert.True(isCreated1);
            Assert.True(isCreated3);
            Assert.False(isCreated2);
            Assert.Equal(0, cashReg1ByDb.Pos);
            Assert.Equal(0, cashReg1ByDb.Cash);
            Assert.Empty(cashReg1ByDb.Shifts);
            Assert.Equal(0, cashReg3ByDb.Cash);
            Assert.Equal(0, cashReg3ByDb.Pos);
        }
    }
}
