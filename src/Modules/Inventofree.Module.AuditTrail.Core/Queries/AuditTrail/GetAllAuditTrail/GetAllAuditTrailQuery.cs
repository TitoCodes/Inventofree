using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using MediatR;

namespace Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAllAuditTrail;

public class GetAllAuditTrailQuery : IRequest<IReadOnlyCollection<AuditTrailDto>>
{
    
}