using AccountantWPF.BaseRepositories;
using AccountantWPF.Data;
using AccountantWPF.Data.Models;

namespace AccountantWPF.Features.CashPosIncomes
{
    public class CashPosService : BaseDeletableRepository<CashPos>
    {
        public CashPosService(AccountantDbContext db) : base(db)
        {
        }
    }
}
