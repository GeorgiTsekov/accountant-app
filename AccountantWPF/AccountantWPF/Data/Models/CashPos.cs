using System.ComponentModel.DataAnnotations.Schema;

using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class CashPos : BaseCashPosDeletableModel
    {
        public CashPos()
        {
            CashRegisters = new List<CashRegister>();
        }

        public int IncomeId { get; set; }
        public virtual Income? Income { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cash => CashRegisters.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Cash);

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pos => CashRegisters.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Pos);
        public virtual ICollection<CashRegister> CashRegisters { get; set; }
    }
}
