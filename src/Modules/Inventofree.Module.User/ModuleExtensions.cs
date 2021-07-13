using Inventofree.Module.User.Core.Extensions;
using Inventofree.Module.User.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.User
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddUserCore()
                .AddUserInfrastructure(configuration);
            return services;
        }
    }
}