using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using MediatR;

namespace Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAuditTrailById;

public class GetAuditTrailByIdQuery : IRequest<AuditTrailDto>
{
    public int Id { get; set; }
}