
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Infrastructure.Persistence;

public class AuditTrailDbContext: ModuleDbContext, IAuditTrailDbContext
{
    public AuditTrailDbContext(DbContextOptions<AuditTrailDbContext> options) : base(options)
    {
    }

    protected override string Schema => "AuditTrail";

    public DbSet<Core.Entities.AuditTrail> AuditTrails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Entities.AuditTrail>();
    }
}