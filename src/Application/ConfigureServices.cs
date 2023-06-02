using Application.Common.Behaviours;
using CustomerAccount.Application.Common.Behaviours;
using CustomerAccount.Application.Common.EventBus;
using CustomerAccount.Application.Common.Interfaces;
using CustomerAccount.Application.Customers.EventHandlers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAccount.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EventBusTopic>(configuration.GetSection(nameof(EventBusTopic)));

        services.AddScoped<IEventInvoke, CustomerEventHandlers>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}