using Inventofree.Module.Item.Core.Extensions;
using Inventofree.Module.Item.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.Item
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddItemModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddItemCore()
                .AddItemInfrastructure(configuration);
            return services;
        }
    }
}