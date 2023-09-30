using Inventofree.Module.Transaction.Core.Extensions;
using Inventofree.Module.Transaction.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.Transaction;

public static class ModuleExtensions
{
    public static IServiceCollection AddTransactionModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTransactionCore()
            .AddTransactionInfrastructure(configuration);
        return services;
    }
}