using BeautySalon.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IClientService, ClientService>()
            .AddTransient<ICountsService, CountsService>()
            .AddTransient<IUserDialog, UserDialogService>()
            .AddTransient<IOrderService, OrdersService>()
            .AddTransient<IServicesService, ServicesService>()
            ;

    }
}
