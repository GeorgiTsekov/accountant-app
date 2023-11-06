using System.Collections.ObjectModel;
using System.Windows.Data;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AccountantWPF.Features.Incomes
{
    public partial class ViewIncomesViewModel : ObservableObject
    {
        private readonly IncomeService _incomeService;

        public ObservableCollection<IncomeViewModel> Incomes { get; } = new();

        public ViewIncomesViewModel(IncomeService incomeService)
        {
            _incomeService = incomeService;
            BindingOperations.EnableCollectionSynchronization(Incomes, new());
            LoadOutstandingIncomes();
        }

        public void LoadOutstandingIncomes()
        {
            Incomes.Clear();

            foreach (var income in _incomeService.AllAsync().GetAwaiter().GetResult())
            {
                Incomes.Add(new IncomeViewModel(income));
            }
        }
    }
}
