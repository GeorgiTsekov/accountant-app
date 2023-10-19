using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class CashRegister : BaseCashPosDeletableModel
    {
        public CashRegister()
        {
            Shifts = new List<Shift>();
        }

        public string CashPosName { get; set; } = string.Empty;
        public virtual CashPos? CashPos { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
