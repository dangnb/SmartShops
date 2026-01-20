using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Shop.Domain.Dappers;

namespace Shop.Persistence.Dapper.DependencyInjection.Extensions;

public static class InfrastructureDapperExtensions
{
    public static IServiceCollection AddInfrastructureDapper(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ✅ 1. Register IDbConnection (1 connection / 1 request)
        services.AddScoped<IDbConnection>(sp =>
            new MySqlConnection(
                configuration.GetConnectionString("ConnectionStrings")));

        // ✅ 2. Register UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // ✅ 3. Auto register all *Repository classes
        services.Scan(scan => scan
            .FromAssemblyOf<UnitOfWork>() // hoặc bất kỳ repository nào
            .AddClasses(classes =>
                classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}
