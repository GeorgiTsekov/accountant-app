using AccountantWPF.BaseRepositories;
using AccountantWPF.Data;
using AccountantWPF.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Features.CashPosIncomes
{
    public class CashPosService : BaseDeletableRepository<CashPos>
    {
        public CashPosService(AccountantDbContext db) : base(db)
        {
        }

        public override async Task<ICollection<CashPos>> AllAsync()
        {
            var entities = await DbSet.Include(x => x.CashRegisters).ThenInclude(a => a.Shifts).ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedOn).ToList();
            return entities;
        }

        public override async Task<CashPos> ByNameAndDateAsync(string name, DateTime date)
        {
            return await DbSet.Include(x => x.CashRegisters).ThenInclude(a => a.Shifts).Where(x => !x.IsDeleted).SingleOrDefaultAsync(x => x.Name == name && x.CreatedOn == date);
        }
    }
}
