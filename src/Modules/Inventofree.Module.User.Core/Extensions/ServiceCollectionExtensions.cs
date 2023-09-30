using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.User.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}