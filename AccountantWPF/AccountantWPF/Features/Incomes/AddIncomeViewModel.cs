using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using AccountantWPF.Data.Models;
using AccountantWPF.Features.CashPosIncomes;
using AccountantWPF.Features.CashRegisters;
using AccountantWPF.Features.Shifts;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AccountantWPF.Features.Incomes
{
    public partial class AddIncomeViewModel : ObservableObject
    {
        private readonly IncomeService _incomeService;
        private readonly CashPosService _cashPosService;
        private readonly CashRegisterService _cashRegisterService;
        private readonly ShiftService _shiftService;

        //private readonly ISnackbarMessageQueue _messageQueue;
        public ObservableCollection<string> IncomeNames { get; } = new();
        public ObservableCollection<string> CashPosNames { get; } = new();
        public ObservableCollection<string> CashRegisterNames { get; } = new();
        public ObservableCollection<string> ShiftNames { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string? _incomeName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private DateTime? _createdOn;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string? _cashPosName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string? _cashRegisterName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string? _shiftName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string? _cashierName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private int? _bonnets;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [Column(TypeName = "decimal(18,2)")]
        private decimal? _shiftCash;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [Column(TypeName = "decimal(18,2)")]
        private decimal? _shiftPos;

        public AddIncomeViewModel(
            IncomeService incomeService,
            CashPosService cashPosService,
            CashRegisterService cashRegisterService,
            ShiftService shiftService)
        {
            _incomeService = incomeService;
            _cashPosService = cashPosService;
            _cashRegisterService = cashRegisterService;
            _shiftService = shiftService;
            LoadOutstandingIncomes();
            //_messageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
        }

        public void LoadOutstandingIncomes()
        {
            IncomeNames.Clear();
            foreach (var income in _incomeService.AllAsync().GetAwaiter().GetResult().Select(x => x.Name))
            {
                IncomeNames.Add(income);
            }

            CashPosNames.Clear();
            foreach (var income in _cashPosService.AllAsync().GetAwaiter().GetResult().Select(x => x.Name))
            {
                CashPosNames.Add(income);
            }

            CashRegisterNames.Clear();
            foreach (var income in _cashRegisterService.AllAsync().GetAwaiter().GetResult().Select(x => x.Name))
            {
                CashRegisterNames.Add(income);
            }

            ShiftNames.Clear();
            foreach (var income in _shiftService.AllAsync().GetAwaiter().GetResult().Select(x => x.Name))
            {
                ShiftNames.Add(income);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private async Task OnSubmit()
        {
            if (string.IsNullOrWhiteSpace(IncomeName) ||
                string.IsNullOrWhiteSpace(CashPosName) ||
                string.IsNullOrWhiteSpace(CashRegisterName) ||
                string.IsNullOrWhiteSpace(ShiftName) ||
                !CreatedOn.HasValue ||
                !ShiftPos.HasValue ||
                !ShiftCash.HasValue)
            {
                return;
            }

            var income = await _incomeService.ByNameAndDateAsync(default, IncomeName, CreatedOn.Value);

            if (income == null)
            {
                income = new Income
                {
                    CreatedOn = CreatedOn.Value.Date,
                    Name = IncomeName
                };

                await _incomeService.CreateAsync(income);
            }

            var cashPos = await _cashPosService.ByNameAndDateAsync(income.Id, CashPosName, CreatedOn.Value);

            if (cashPos == null || cashPos.IncomeId != income.Id)
            {
                cashPos = new CashPos
                {
                    CreatedOn = CreatedOn.Value.Date,
                    Name = CashPosName,
                    IncomeId = income.Id,
                    ParentId = income.Id
                };

                await _cashPosService.CreateAsync(cashPos);
            }
            else
            {
                //_messageQueue.Enqueue("There is a asdas with same name and date!");
            }

            var cashRegister = await _cashRegisterService.ByNameAndDateAsync(cashPos.Id, CashRegisterName, CreatedOn.Value);

            if (cashRegister == null || cashRegister.CashPosId != cashPos.Id)
            {
                cashRegister = new CashRegister
                {
                    CreatedOn = CreatedOn.Value.Date,
                    Name = CashRegisterName,
                    CashPosId = cashPos.Id,
                    ParentId = cashPos.Id
                };

                await _cashRegisterService.CreateAsync(cashRegister);
            }
            else
            {
                //_messageQueue.Enqueue("There is a asdas with same name and date!");
            }

            var shift = await _shiftService.ByNameAndDateAsync(cashRegister.Id, ShiftName, CreatedOn.Value);

            if (shift == null || shift.CashRegisterId != cashRegister.Id)
            {
                shift = new Shift
                {
                    CreatedOn = CreatedOn.Value.Date,
                    Name = ShiftName,
                    CashierName = CashierName,
                    Bonnets = Bonnets,
                    Pos = ShiftPos.Value,
                    Cash = ShiftCash.Value,
                    CashRegisterId = cashRegister.Id,
                    ParentId = cashRegister.Id
                };

                var created = await _shiftService.CreateAsync(shift);

                if (created)
                {
                    //_messageQueue.Enqueue("Saved!");
                }
                else
                {
                    //_messageQueue.Enqueue("Incorect Format!");
                }
            }
            else
            {
                //_messageQueue.Enqueue("There is a shift with same name and date!");
            }

            IncomeName = string.Empty;
            CashPosName = string.Empty;
            CashRegisterName = string.Empty;
            ShiftName = string.Empty;
            CashierName = string.Empty;
            Bonnets = null;
            CreatedOn = null;
            ShiftPos = null;
            ShiftCash = null;
        }

        private bool CanSubmit()
            => !string.IsNullOrWhiteSpace(IncomeName) &&
            !string.IsNullOrWhiteSpace(ShiftName) &&
            !string.IsNullOrWhiteSpace(CashRegisterName) &&
            !string.IsNullOrWhiteSpace(CashPosName) &&
                CreatedOn.HasValue &&
                ShiftPos.HasValue &&
                ShiftCash.HasValue;
    }
}
