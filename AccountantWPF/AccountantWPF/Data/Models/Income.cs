using System.ComponentModel.DataAnnotations.Schema;

using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class Income : BaseCashPosDeletableModel
    {
        public Income()
        {
            CashPosIncomes = new List<CashPos>();
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cash => CashPosIncomes.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Cash);

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pos => CashPosIncomes.Where(x => !x.IsDeleted && x.CreatedOn == CreatedOn).Sum(x => x.Pos);

        public virtual ICollection<CashPos> CashPosIncomes { get; set; }
        // TODO: bankIncome
    }
}
