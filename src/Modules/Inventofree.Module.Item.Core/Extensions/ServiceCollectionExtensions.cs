using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.Item.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItemCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}