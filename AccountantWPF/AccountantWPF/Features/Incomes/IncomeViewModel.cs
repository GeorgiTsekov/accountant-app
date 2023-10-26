using AccountantWPF.Data.Models;

using CommunityToolkit.Mvvm.Input;

namespace AccountantWPF.Features.Incomes
{
    public partial class IncomeViewModel
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Pos { get; }
        public decimal Cash { get; }
        public DateTime CreatedOn { get; }
        public ICollection<CashPos> CashPosIncomes { get; set; }

        public IncomeViewModel(Income income)
        {
            Id = income.Id;
            Name = income.Name;
            Pos = income.Pos;
            Cash = income.Cash;
            CreatedOn = income.CreatedOn;
            CashPosIncomes = income.CashPosIncomes;
        }

        [RelayCommand]
        public void OnAllAsync()
        {
            if (CashPosIncomes.Count > 0)
            {
                //TODO:
            }
        }
    }
}
