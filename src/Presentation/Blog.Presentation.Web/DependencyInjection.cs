using Blog.Infrastructure.Options;
using Blog.Presentation.Web.Resources.ExceptionHandlers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blog.Presentation.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHttpContextAccessor();


        // Options Pattern Configuration
        services.Configure<AzureOptions>(configuration.GetSection("Azure"));
        services.Configure<EmailOptions>(configuration.GetSection("Email"));

        //Authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Auth";
            });
        
        return services;
    }

    public static WebApplication UsePresentationServices(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { });
        return app;
    }
}