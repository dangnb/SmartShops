using Shop.Domain.Dappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Shop.Domain.Dappers.Repositories;
using Shop.Persistence.Dapper.Repositories;

namespace Shop.Persistence.Dapper.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDapper(this IServiceCollection servies, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("ConnectionStrings");
        servies.AddTransient<IDbConnection>(sp => new MySqlConnection(connectionString))
               .AddTransient<IProductRepository, ProductRepository>()
               .AddTransient<IPaymentRepository, PaymentRepository>()
               .AddTransient<IUserRepository, UserRepository>()
               .AddTransient<IDistrictRepository, DistrictRepository>()
               .AddTransient<ICustomerRepository, CustomerRepository>()
               
               .AddTransient<IUnitOfWork, UnitOfWork>();
    }
}
