namespace AccountantWPF.Features.Incomes
{
    /// <summary>
    /// Interaction logic for ViewIncomesView.xaml
    /// </summary>
    public partial class ViewIncomesView
    {
        public ViewIncomesView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ViewIncomesViewModel viewModel)
            {
                viewModel.LoadOutstandingIncomes();
            }
        }
    }
}
