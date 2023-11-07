using System.Collections.ObjectModel;
using System.Windows.Data;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AccountantWPF.Features.Incomes
{
    public partial class SingleIncomeViewModel : ObservableObject
    {
        public ObservableCollection<string> IncomeNames { get; } = new();

        private readonly IncomeService _incomeService;

        public ObservableCollection<IncomeViewModel> Income { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string? _incomeName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private DateTime? _createdOn;

        public SingleIncomeViewModel(IncomeService incomeService)
        {
            _incomeService = incomeService;
            BindingOperations.EnableCollectionSynchronization(Income, new());
            LoadOutstandingIncomes();
        }

        public void LoadOutstandingIncomes()
        {
            IncomeNames.Clear();
            foreach (var name in _incomeService.AllAsync().GetAwaiter().GetResult().Select(x => x.Name))
            {
                IncomeNames.Add(name);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private async Task OnSubmit()
        {
            Income.Clear();
            if (string.IsNullOrWhiteSpace(IncomeName) || !CreatedOn.HasValue)
            {
                return;
            }

            var income = await _incomeService.ByNameAndDateAsync(default, IncomeName, CreatedOn.Value);

            if (income == null)
            {
                IncomeName = string.Empty;
                CreatedOn = null;
                // message
                return;
            }
            Income.Add(new IncomeViewModel(income));
            // message

            IncomeName = string.Empty;
            CreatedOn = null;
        }

        private bool CanSubmit() => !string.IsNullOrWhiteSpace(IncomeName) && CreatedOn.HasValue;
    }
}
