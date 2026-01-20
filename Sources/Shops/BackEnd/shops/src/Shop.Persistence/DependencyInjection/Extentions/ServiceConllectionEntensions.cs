using Shop.Domain.Entities.Identity;
using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Persistence.DependencyInjection.Options;
using Shop.Persistence.Repositoty;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Shop.Persistence.DependencyInjection.Extentions;

public static class ServiceConllectionEntensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptionsMonitor<MySqlRetryOptions>>();
            #region ============== SQL-SERVER-STRATEGY-1 ==============
            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseMySql(
                 configuration.GetConnectionString("ConnectionStrings"),
                 new MySqlServerVersion(new Version(8, 0, 21)),
                         optionsBuilder
                  => optionsBuilder.ExecutionStrategy(
                          dependencies => new MySqlRetryingExecutionStrategy(
                              dependencies: dependencies,
                              maxRetryCount: options.CurrentValue.MaxRetryCount,
                              maxRetryDelay: options.CurrentValue.MaxRetryDelay,
                              errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
                      .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name)
                 )

        // Thêm một interceptor cho DbContext.
        // Interceptor này sẽ được gọi trong quá trình lưu dữ liệu (SaveChanges) của Entity Framework Core.
        // Cụ thể, DomainEventsInterceptor sẽ thực hiện công việc lắng nghe các sự kiện (Domain Events) 
        // mà các đối tượng (Entities) trong Domain có thể phát sinh khi dữ liệu được lưu vào cơ sở dữ liệu.
        // Khi các Domain Events này được phát hiện, chúng sẽ được "publish" (gửi đi) để các Event Handlers có thể xử lý,
        // ví dụ như gửi email, cập nhật trạng thái trong các hệ thống khác, v.v.
        // Việc này giúp việc xử lý sự kiện và logic nghiệp vụ được tách biệt và linh động hơn,
        // giữ cho mã nguồn sạch sẽ và dễ duy trì theo nguyên lý Clean Architecture.
        // 
        // provider.GetRequiredService<DomainEventsInterceptor>() sẽ yêu cầu DI container
        // cung cấp đối tượng DomainEventsInterceptor đã được đăng ký trước đó.
        // Điều này đảm bảo rằng interceptor sẽ được thêm vào trong pipeline xử lý của DbContext.
        .AddInterceptors(provider.GetRequiredService<DomainEventsInterceptor>())
        ;

            #endregion ============== SQL-SERVER-STRATEGY-1 ==============

            #region ============== SQL-SERVER-STRATEGY-2 ==============

            //builder
            //.EnableDetailedErrors(true)
            //.EnableSensitiveDataLogging(true)
            //.UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            //.UseSqlServer(
            //    connectionString: configuration.GetConnectionString("ConnectionStrings"),
            //        sqlServerOptionsAction: optionsBuilder
            //            => optionsBuilder
            //            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));

            #endregion ============== SQL-SERVER-STRATEGY-2 ==============
        });

        services.AddIdentityCore<AppUser>()
        .AddRoles<AppRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); //default 5
            options.Lockout.MaxFailedAccessAttempts = 3;//default 5
        });
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        => services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork))
        .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

    public static OptionsBuilder<MySqlRetryOptions> ConfigureMySqlRetryOptions(this IServiceCollection services, IConfigurationSection section)
    {
        return services
                  .AddOptions<MySqlRetryOptions>()
                  .Bind(section)
                  .ValidateDataAnnotations()
                  .ValidateOnStart();
    }
}
