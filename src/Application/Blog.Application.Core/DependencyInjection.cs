using System.Reflection;
using Blog.Application.Core.Behaviours;
using Blog.Application.Core.Validators.SlugValidator;
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
        
        // Validators
        services.AddScoped<ISlugValidator, SlugValidator>();
        
        return services;
    }
}