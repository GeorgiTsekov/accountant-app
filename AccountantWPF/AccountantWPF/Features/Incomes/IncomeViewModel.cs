using System.ComponentModel.DataAnnotations.Schema;

using AccountantWPF.Data.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AccountantWPF.Features.Incomes
{
    public partial class IncomeViewModel : ObservableObject
    {
        public ICollection<CashPos> CashPosIncomes { get; }
        public string Name { get; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pos { get; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cash { get; }
        public DateTime CreatedOn { get; }

        public IncomeViewModel(Income income)
        {
            Name = income.Name;
            Pos = income.Pos;
            Cash = income.Cash;
            CreatedOn = income.CreatedOn;
            CashPosIncomes = income.CashPosIncomes;
        }
    }
}
