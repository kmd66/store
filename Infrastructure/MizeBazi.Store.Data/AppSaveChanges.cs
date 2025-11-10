using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data;

public class AppSaveChanges : SaveChangesInterceptor
{
    private void UpdateTimestamps(DbContext context)
    {
        var entries = context.ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.Date = DateTime.UtcNow;
                if (entity.UnicId == Guid.Empty)
                    entity.UnicId = Guid.NewGuid();
            }

            if (entityEntry.State == EntityState.Deleted && entity is SoftDeleteEntity softDeleteEntity)
            {
                entityEntry.State = EntityState.Modified;
                softDeleteEntity.IsDeleted = true;
                softDeleteEntity.DeletedDate = DateTime.UtcNow;
            }
        }
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateTimestamps(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateTimestamps(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
