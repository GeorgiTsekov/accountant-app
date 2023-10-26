using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class Income : BaseDeletableModel
    {
        public Income()
        {
            CashPosIncomes = new List<CashPos>();
        }

        public decimal Cash => CashPosIncomes.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Cash);
        public decimal Pos => CashPosIncomes.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Pos);
        public virtual ICollection<CashPos> CashPosIncomes { get; set; }
        // TODO: bankIncome
    }
}
