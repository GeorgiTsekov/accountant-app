using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class Shift : BaseCashPosDeletableModel
    {
        public string? CashierName { get; set; }
        public int? Bonnets { get; set; }
        public string CashRegisterName { get; set; } = string.Empty;
        public virtual CashRegister? CashRegister { get; set; }
    }
}
