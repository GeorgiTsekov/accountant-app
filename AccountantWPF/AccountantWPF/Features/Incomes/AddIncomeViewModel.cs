using AccountantWPF.Data.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

namespace AccountantWPF.Features.Incomes
{
    public partial class AddIncomeViewModel : ObservableObject
    {
        private readonly IncomeService _incomeService;
        //private readonly ISnackbarMessageQueue _messageQueue;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string? _name;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private DateTime? _createdOn;
        public AddIncomeViewModel(IncomeService incomeService, ISnackbarMessageQueue messageQueue)
        {
            _incomeService = incomeService;
            //_messageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
        }

        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private async Task OnSubmit()
        {
            if (string.IsNullOrWhiteSpace(Name) || !CreatedOn.HasValue)
            {
                return;
            }
            var newIncome = new Income
            {
                CreatedOn = CreatedOn.Value.Date,
                Name = Name
            };

            var created = await _incomeService.CreateAsync(newIncome);

            if (created)
            {
                Name = string.Empty;
                CreatedOn = null;
                //_messageQueue.Enqueue("Saved!");
            }
            else
            {
                //_messageQueue.Enqueue("Incorect Format!");
            }
        }

        private bool CanSubmit() => !string.IsNullOrWhiteSpace(Name) && CreatedOn.HasValue;
    }
}
