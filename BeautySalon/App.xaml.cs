using BeautySalon.DAL.Context;
using BeautySalon.Data;
using BeautySalon.Services;
using BeautySalon.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;

namespace BeautySalon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window? ActiveWindow => Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w.IsActive);

        public static Window? FocusedWindow => Current.Windows.
            OfType<Window>()
            .FirstOrDefault(w => w.IsFocused);

        public static Window? CurrentWindow => FocusedWindow ?? ActiveWindow;


        private static IHost? __Host;
        public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddDatabase(host.Configuration.GetSection("Database"))
            .AddServices()
            .AddViewModels()
            ;


        protected override void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            var db = host.Services.GetRequiredService<BeautySalonDB>();
            db.Database.Migrate();


            MainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();


            base.OnStartup(e);
            host.Start();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            var host = Host;
            base.OnExit(e);
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }
    }
}
