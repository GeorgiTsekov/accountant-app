using AccountantWPF.BaseRepositories;
using AccountantWPF.Data;
using AccountantWPF.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Features.Incomes
{
    public class IncomeService : BaseDeletableRepository<Income>
    {
        public IncomeService(AccountantDbContext db) : base(db)
        {
        }

        public override async Task<ICollection<Income>> AllAsync()
        {
            var entities = await DbSet.Include(x => x.CashPosIncomes).ThenInclude(x => x.CashRegisters).ThenInclude(a => a.Shifts).ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedOn).ToList();
            return entities;
        }

        public override async Task<Income> ByNameAndDateAsync(string name, DateTime date)
        {
            return await DbSet.Include(x => x.CashPosIncomes).ThenInclude(x => x.CashRegisters).ThenInclude(a => a.Shifts).Where(x => !x.IsDeleted).SingleOrDefaultAsync(x => x.Name == name && x.CreatedOn == date);
        }
    }
}
