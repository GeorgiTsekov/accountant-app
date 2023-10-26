using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class CashPos : BaseDeletableModel
    {
        public CashPos()
        {
            CashRegisters = new List<CashRegister>();
        }

        public int IncomeId { get; set; }
        public virtual Income? Income { get; set; }
        public decimal Cash => CashRegisters.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Cash);
        public decimal Pos => CashRegisters.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Pos);
        public virtual ICollection<CashRegister> CashRegisters { get; set; }
    }
}
