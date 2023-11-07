using System.Windows;
using System.Windows.Threading;

using AccountantWPF.Data;
using AccountantWPF.Features.CashPosIncomes;
using AccountantWPF.Features.CashRegisters;
using AccountantWPF.Features.Shifts;
using AccountantWPF.Features.Incomes;

using CommunityToolkit.Mvvm.Messaging;

using MaterialDesignThemes.Wpf;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccountantWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    [STAThread]
    public static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        host.Start();
        using (var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        using (var context = scope.ServiceProvider.GetRequiredService<AccountantDbContext>())
        {
            context.Database.Migrate();
        }

        App app = new();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder)
            => configurationBuilder.AddUserSecrets(typeof(App).Assembly))
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<WeakReferenceMessenger>();
            services.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());

            services.AddSingleton(_ => Current.Dispatcher);

            services.AddDbContext<AccountantDbContext>();

            services.AddTransient<ISnackbarMessageQueue>(provider =>
            {
                Dispatcher dispatcher = provider.GetRequiredService<Dispatcher>();
                return new SnackbarMessageQueue(TimeSpan.FromSeconds(3.0), dispatcher);
            });

            services.AddSingleton<IncomeService>();
            services.AddSingleton<ViewIncomesViewModel>();
            services.AddSingleton<SingleIncomeViewModel>();
            services.AddSingleton<AddIncomeViewModel>();
            services.AddSingleton<CashPosService>();
            services.AddSingleton<CashRegisterService>();
            services.AddSingleton<ShiftService>();
        });
}
