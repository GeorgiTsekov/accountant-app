using AccountantWPF.Data.Models;

namespace AccountantWPF.Features.Incomes
{
    public class IncomeViewModel
    {
        public string Name { get; }
        public decimal Pos { get; }
        public decimal Cash { get; }
        public DateTime CreatedOn { get; }

        public IncomeViewModel(Income income)
        {
            Name = income.Name;
            Pos = income.Pos;
            Cash = income.Cash;
            CreatedOn = income.CreatedOn;
        }
    }
}
