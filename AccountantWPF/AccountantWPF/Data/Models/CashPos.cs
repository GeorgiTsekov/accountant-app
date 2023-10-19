using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class CashPos : BaseCashPosDeletableModel
    {
        public CashPos()
        {
            CashRegisters = new List<CashRegister>();
        }

        public string IncomeName { get; set; } = string.Empty;
        public virtual Income? Income { get; set; }
        public virtual ICollection<CashRegister> CashRegisters { get; set; }
    }
}
