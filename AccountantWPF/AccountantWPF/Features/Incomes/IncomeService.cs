using AccountantWPF.BaseRepositories;
using AccountantWPF.Data;
using AccountantWPF.Data.Models;

namespace AccountantWPF.Features.Incomes
{
    public class IncomeService : BaseDeletableRepository<Income>
    {
        public IncomeService(AccountantDbContext db) : base(db)
        {
        }
    }
}
