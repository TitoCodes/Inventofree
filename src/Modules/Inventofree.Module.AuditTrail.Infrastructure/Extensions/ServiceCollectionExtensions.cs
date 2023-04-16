using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Infrastructure.Persistence;
using Inventofree.Shared.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.AuditTrail.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditTrailInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddDatabaseContext<AuditTrailDbContext>(config)
            .AddScoped<IAuditTrailDbContext, AuditTrailDbContext>();
        return services;
    }
}