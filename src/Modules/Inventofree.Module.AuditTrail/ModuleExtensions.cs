using Inventofree.Module.AuditTrail.Core.Extensions;
using Inventofree.Module.AuditTrail.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.AuditTrail;

public static class ModuleExtensions
{
    public static IServiceCollection AddAuditTrailModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuditTrailCore()
            .AddAuditTrailInfrastructure(configuration);
        return services;
    }
}