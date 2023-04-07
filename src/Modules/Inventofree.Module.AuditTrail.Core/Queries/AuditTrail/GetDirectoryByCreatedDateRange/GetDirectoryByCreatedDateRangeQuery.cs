using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using MediatR;

namespace Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetDirectoryByCreatedDateRange;

public class GetDirectoryByCreatedDateRangeQuery : IRequest<IReadOnlyCollection<AuditTrailDto>>
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}