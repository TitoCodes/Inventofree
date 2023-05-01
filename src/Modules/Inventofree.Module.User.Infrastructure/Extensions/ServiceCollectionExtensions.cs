using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Inventofree.Shared.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.User.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<UserDbContext>(config)
                .AddScoped<IUserDbContext, UserDbContext>();
            return services;
        }
    }
}