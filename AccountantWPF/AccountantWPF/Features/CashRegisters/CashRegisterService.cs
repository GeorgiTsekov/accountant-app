using AccountantWPF.BaseRepositories;
using AccountantWPF.Data;
using AccountantWPF.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AccountantWPF.Features.CashRegisters
{
    public class CashRegisterService : BaseDeletableRepository<CashRegister>
    {
        public CashRegisterService(AccountantDbContext db) : base(db)
        {
        }

        public override async Task<ICollection<CashRegister>> AllAsync()
        {
            var entities = await DbSet.Include(a => a.Shifts).ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedOn).ToList();
            return entities;
        }

        public override async Task<CashRegister> ByNameAndDateAsync(string name, DateTime date)
        {
            return await DbSet.Include(a => a.Shifts).Where(x => !x.IsDeleted).SingleOrDefaultAsync(x => x.Name == name && x.CreatedOn == date);
        }
    }
}
