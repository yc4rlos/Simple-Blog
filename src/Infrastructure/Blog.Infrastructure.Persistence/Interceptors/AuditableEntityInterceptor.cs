using Blog.Domain.Abstractions.Services;
using Blog.Domain.Models;

namespace Blog.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor(IAuthService authService)
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? dbContext)
    {
        if (dbContext == null) return;

        var userId = authService.GetCurrentUserId();

        if (userId == null)
            return;

        foreach (var entry in dbContext.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedById = (int)userId;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedById = (int)userId;
                    break;
            }
        }
    }
}