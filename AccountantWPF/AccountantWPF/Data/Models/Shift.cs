using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class Shift : BaseDeletableModel
    {
        public string? CashierName { get; set; }
        public int? Bonnets { get; set; }
        public int CashRegisterId { get; set; }
        public virtual CashRegister? CashRegister { get; set; }
    }
}
