using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class Income : BaseCashPosDeletableModel
    {
        public Income()
        {
            CashPosIncomes = new List<CashPos>();
        }

        public virtual ICollection<CashPos> CashPosIncomes { get; set; }
        // TODO: bankIncome
    }
}
