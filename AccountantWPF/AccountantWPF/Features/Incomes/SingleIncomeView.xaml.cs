namespace AccountantWPF.Features.Incomes
{
    /// <summary>
    /// Interaction logic for ViewIncomeView.xaml
    /// </summary>
    public partial class SingleIncomeView
    {
        public SingleIncomeView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is SingleIncomeViewModel viewModel)
            {
                viewModel.LoadOutstandingIncomes();
            }
        }
    }
}
