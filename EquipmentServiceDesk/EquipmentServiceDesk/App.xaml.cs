using EquipmentServiceDesk.Data;
using EquipmentServiceDesk.Repositories;
using EquipmentServiceDesk.Services;
using EquipmentServiceDesk.ViewModels;
using EquipmentServiceDesk.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Windows;

namespace EquipmentServiceDesk
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        // Логирование
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(configuration["Logging:LogFilePath"])
            .CreateLogger();

        // DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Services
        services.AddScoped<AuthService>();
        services.AddScoped<RequestService>();
        services.AddScoped<CurrentUserService>();

        // ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<RequestsViewModel>();

        // Views
        services.AddTransient<LoginView>();
        services.AddTransient<MainWindow>();
        services.AddTransient<RequestsView>();
    })
    .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            var login = AppHost.Services.GetRequiredService<LoginView>();
            login.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}