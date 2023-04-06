using Inventofree.Shared.Core.Entities;

namespace Inventofree.Module.AuditTrail.Core.Entities;

public class AuditTrail : BaseEntity
{
    public string? Action { get; set; }
    public string? Details { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
}