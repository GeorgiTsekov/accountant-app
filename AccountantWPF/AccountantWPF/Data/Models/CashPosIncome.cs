using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class CashPosIncome : BaseCashPosNameDeletableModel
    {
        public CashPosIncome()
        {
            CashRegisters = new List<CashRegister>();
        }

        public int IncomeId { get; set; }
        public virtual Income? Income { get; set; }
        public virtual ICollection<CashRegister> CashRegisters { get; set; }
    }
}
