using MediatR;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.DeleteAuditTrail;

public class DeleteAuditTrailCommand : IRequest<Unit>
{
    public int Id { get; set; }
}