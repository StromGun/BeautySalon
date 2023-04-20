using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BeautySalon.DAL.Context;
using System;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Data
{
    internal static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<BeautySalonDB>(opt =>
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case null: throw new InvalidOperationException("Не определен тип БД");
                    default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

                    case "MSSQL":
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                }
            })
            ;
    }
}
