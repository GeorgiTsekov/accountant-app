using System.ComponentModel.DataAnnotations.Schema;

using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class CashRegister : BaseCashPosDeletableModel
    {
        public CashRegister()
        {
            Shifts = new List<Shift>();
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cash => Shifts.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Cash);

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pos => Shifts.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Pos);

        public int CashPosId { get; set; }
        public virtual CashPos? CashPos { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
