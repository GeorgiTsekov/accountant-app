using System.ComponentModel.DataAnnotations.Schema;

using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class Shift : BaseDeletableModel
    {
        public string? CashierName { get; set; }
        public int? Bonnets { get; set; }
        public int CashRegisterId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cash { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pos { get; set; }
        public virtual CashRegister? CashRegister { get; set; }
    }
}
