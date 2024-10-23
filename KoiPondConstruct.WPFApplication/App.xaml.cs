using KoiPondConstruct.Service;
using KoiPondConstruct.Service.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace KoiPondConstruct.WPFApplication
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            // Set up the host for Dependency Injection
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                    services.AddHttpContextAccessor();
                    // Register your services here
                    services.ConfigureBALServices(); // Custom service configuration

                    // Register ICustomerRequestService and MainWindow
                    services.AddSingleton<ICustomerRequestService, CustomerRequestService>();

                    // Register MainWindow for DI
                    services.AddTransient<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            // Start the host
            await _host.StartAsync();

            // Resolve MainWindow from the DI container and show it
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // Gracefully stop the host and dispose of resources
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
