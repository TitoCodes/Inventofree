using Inventofree.Module.Transaction.Core.Abstractions;
using Inventofree.Module.Transaction.Infrastructure.Persistence;
using Inventofree.Shared.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.Transaction.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTransactionInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddDatabaseContext<TransactionDbContext>(config)
            .AddScoped<ITransactionDbContext, TransactionDbContext>();
        return services;
    }
}