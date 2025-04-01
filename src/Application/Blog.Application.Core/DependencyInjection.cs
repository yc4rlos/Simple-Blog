using System.Reflection;
using Blog.Application.Core.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Blog.Application.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(assembly);
            c.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}