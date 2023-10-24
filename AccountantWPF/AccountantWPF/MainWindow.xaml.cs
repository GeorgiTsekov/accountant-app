using System.Windows.Input;

using AccountantWPF.Features.Incomes;

namespace AccountantWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow(ViewIncomesViewModel viewIncomesModelModel, AddIncomeViewModel addIncomeViewModel)
    {
        //DataContext = viewModel;

        InitializeComponent();

        AddIncomeView.DataContext = addIncomeViewModel;
        ViewIncomesView.DataContext = viewIncomesModelModel;

        CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnClose));
    }

    private void OnClose(object sender, ExecutedRoutedEventArgs e)
    {
        Close();
    }
}
