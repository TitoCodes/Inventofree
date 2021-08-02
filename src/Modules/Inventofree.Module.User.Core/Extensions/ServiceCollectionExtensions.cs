using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.User.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}