using AccountantWPF.Data.Models;
using AccountantWPF.Features.CashPosIncomes;
using AccountantWPF.Features.CashRegisters;
using AccountantWPF.Features.Shifts;
using AccountantWPF.Incomes;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Tests
{
    public class ShiftServiceTests : TestBase
    {
        [Fact]
        public async Task Add_Correct_Shit_ToDataBase()
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
            var shiftService = new ShiftService(dbContext);
            await incomeService.CreateAsync(income);

            var cashPos = new CashPos
            {
                Name = "Autoban",
                IncomeId = income.Id,
                CreatedOn = DateTime.Now.Date,
            };

            await cashPosService.CreateAsync(cashPos);

            var cashRegister = new CashRegister
            {
                Name = "CashRegister1",
                CashPosId = cashPos.Id,
                CreatedOn = DateTime.Now.Date,
            };

            await cashRegisterService.CreateAsync(cashRegister);

            var shift1 = new Shift
            {
                CashierName = "Pesho",
                Name = "First",
                CashRegisterId = cashRegister.Id,
                CreatedOn = DateTime.Now.Date,
                Pos = 100.23M,
                Cash = 300
            };

            var shift2 = new Shift
            {
                CashierName = "Pesho",
                Name = "First",
                CashRegisterId = cashRegister.Id,
                CreatedOn = DateTime.Now.Date,
                Pos = 100.23M,
                Cash = 300
            };

            var shift3 = new Shift
            {
                CashierName = "Gosho",
                Name = "Second",
                CashRegisterId = cashRegister.Id,
                CreatedOn = DateTime.Now.Date,
                Pos = 200.23M,
                Cash = 600
            };

            // Act
            var isCreated1 = await shiftService.CreateAsync(shift1);
            var isCreated2 = await shiftService.CreateAsync(shift2);
            var isCreated3 = await shiftService.CreateAsync(shift3);
            var shift1ByDb = await dbContext.Shifts.Where(x => x.Name == "First").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);
            var shift3ByDb = await dbContext.Shifts.Where(x => x.Name == "Second").SingleOrDefaultAsync(x => x.CreatedOn == DateTime.Now.Date);

            // Assert
            Assert.NotNull(shift1ByDb);
            Assert.NotNull(shift3ByDb);
            Assert.Equal("First", shift1ByDb.Name);
            Assert.True(isCreated1);
            Assert.True(isCreated3);
            Assert.False(isCreated2);
            Assert.Equal(100.23M, shift1ByDb.Pos);
            Assert.Equal(300, shift1ByDb.Cash);
            Assert.Equal(200.23M, shift3ByDb.Pos);
            Assert.Equal(600, shift3ByDb.Cash);
            Assert.Equal(900, income.Cash);
            Assert.Equal(300.46M, income.Pos);
        }
    }
}
