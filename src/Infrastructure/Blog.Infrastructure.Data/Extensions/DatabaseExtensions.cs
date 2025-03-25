using Blog.Infrastructure.Data.Context;
using Blog.Infrastructure.Data.InitialData;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await SeedAsync(context);
    }
    
    public static void InitializeDatabaseAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        context.Database.MigrateAsync().GetAwaiter().GetResult();
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedUserAsync(context);
        await SeedTagsAsync(context);
    }

    private static async Task SeedTagsAsync(ApplicationDbContext context)
    {
        if (!await context.Tags.AnyAsync())
        {
            await context.Tags.AddRangeAsync(TagInitialData.Data);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedUserAsync(ApplicationDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            await context.Users.AddRangeAsync(UserInitialData.Data);
            await context.SaveChangesAsync();
        }
    }
}