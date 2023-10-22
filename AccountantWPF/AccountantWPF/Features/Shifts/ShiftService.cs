using AccountantWPF.BaseRepositories;
using AccountantWPF.Data;
using AccountantWPF.Data.Models;

namespace AccountantWPF.Features.Shifts
{
    public class ShiftService : BaseDeletableRepository<Shift>
    {
        public ShiftService(AccountantDbContext db) : base(db)
        {
        }
    }
}
