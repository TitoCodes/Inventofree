using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Abstractions;

public interface IAuditTrailDbContext
{
    public DbSet<Core.Entities.AuditTrail> AuditTrails { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}