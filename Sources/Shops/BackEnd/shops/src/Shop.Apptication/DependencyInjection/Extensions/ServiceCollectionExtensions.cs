using Shop.Application.Behaviors;
using Shop.Application.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Application.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services) =>
        services.AddMediatR(options =>
        options.RegisterServicesFromAssembly(AssemblyReference.assembly))
        //.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
        .AddValidatorsFromAssembly(Contract.AssemblyReference.assembly, includeInternalTypes: true);

    public static IServiceCollection AddConfigureAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(typeof(ServiceProfile));

}
