using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class CashRegister : BaseCashPosNameDeletableModel
    {
        public CashRegister()
        {
            Shifts = new List<Shift>();
        }

        public int CashPosIncomeId { get; set; }
        public virtual CashPosIncome? CashPosIncome { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
