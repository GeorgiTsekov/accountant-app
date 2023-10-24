using System.Collections.ObjectModel;
using System.Windows.Data;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        }

        [RelayCommand]
        public async Task OnRefresh()
        {
            await LoadOutstandingIncomes();
        }

        public async Task LoadOutstandingIncomes()
        {
            Incomes.Clear();

            foreach (var income in await _incomeService.AllAsync())
            {
                Incomes.Add(new IncomeViewModel(income));
            }
        }
    }
}
