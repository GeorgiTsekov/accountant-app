using AccountantWPF.Data.BaseModels;

namespace AccountantWPF.Data.Models
{
    public class Income : BaseCashPosNameDeletableModel
    {
        public Income()
        {
            CashPosIncomes = new List<CashPosIncome>();
        }

        public virtual ICollection<CashPosIncome> CashPosIncomes { get; set; }
        // TODO: bankIncome
    }
}
