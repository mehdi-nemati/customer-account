using CustomerAccount.Application.Common.Interfaces;
using CustomerAccount.Infrastructure.Configurations;
using CustomerAccount.Infrastructure.Persistence;
using CustomerAccount.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAccount.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped);

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        var dataContext = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
        dataContext.Database.EnsureCreated();

        services.Configure<MongoDbConfig>(configuration.GetSection(nameof(MongoDbConfig)));

        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}
