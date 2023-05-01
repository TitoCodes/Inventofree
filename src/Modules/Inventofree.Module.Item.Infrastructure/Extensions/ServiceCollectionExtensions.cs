using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Infrastructure.Persistence;
using Inventofree.Shared.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.Item.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItemInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<ItemDbContext>(config)
                .AddScoped<IItemDbContext, ItemDbContext>();
            return services;
        }
    }
}