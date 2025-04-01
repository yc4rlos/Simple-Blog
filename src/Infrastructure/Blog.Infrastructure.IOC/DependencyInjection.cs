using Blog.Application.Core.Data;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Abstractions.Services;
using Blog.Infrastructure.Data.Context;
using Blog.Infrastructure.Options;
using Blog.Infrastructure.Persistence.Interceptors;
using Blog.Infrastructure.Persistence.Repositories;
using Blog.Infrastructure.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, ConnectionStringsOptions connectionStringsOptions)
    {
        services.AddScoped<AuditableEntityInterceptor>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();

        // Repositories
        services.AddScoped<ISlugRepository, SlugRepository>();

        // Database
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();

            options.AddInterceptors(interceptor)
                .UseSqlServer(connectionStringsOptions.DefaultConnection);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


        return services;
    }
}