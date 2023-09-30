using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventofree.Module.Item.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddItemCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}