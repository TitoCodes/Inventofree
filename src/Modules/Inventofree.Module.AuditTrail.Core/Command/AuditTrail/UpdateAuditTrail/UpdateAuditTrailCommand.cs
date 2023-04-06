using MediatR;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;

public class UpdateAuditTrailCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? Action { get; set; }
    public string? Details { get; set; }
    public long UpdatedBy { get; set; }
}