using AccountantWPF.BaseRepositories;
using AccountantWPF.Data;
using AccountantWPF.Data.Models;

namespace AccountantWPF.Features.CashRegisters
{
    public class CashRegisterService : BaseDeletableRepository<CashRegister>
    {
        public CashRegisterService(AccountantDbContext db) : base(db)
        {
        }
    }
}
