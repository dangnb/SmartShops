using Microsoft.Extensions.DependencyInjection;
using Shop.Contract;

namespace Shop.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureInfrastructure(this IServiceCollection services) =>
        services.AddHttpContextAccessor().AddSingleton<ICurrentUser, CurrentUser>();
}
