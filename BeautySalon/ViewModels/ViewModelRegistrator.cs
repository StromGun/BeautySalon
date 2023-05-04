using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddTransient<MainViewModel>()
            .AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            })
            .AddTransient<ClientViewModel>()
            .AddTransient<NavigationViewModel>()
            .AddTransient<OrdersViewModels>()
            .AddTransient<ServicesListViewModel>()
            ;
    }
}
