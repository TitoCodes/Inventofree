using MediatR;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;

public class AddAuditTrailCommand: IRequest<long>
{
    public string? Action { get; set; }
    public string? Details { get; set; }
    public long CreatedBy { get; set; }
}