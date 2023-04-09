using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetDirectoryByCreatedDateRange;

public class GetDirectoryByCreatedDateRangeQueryHandler: IRequestHandler<GetDirectoryByCreatedDateRangeQuery, IReadOnlyCollection<AuditTrailDto>>
{
    private readonly IAuditTrailDbContext _auditTrailDbContext;
    private readonly IMapper _mapper;

    public GetDirectoryByCreatedDateRangeQueryHandler(IAuditTrailDbContext auditTrailDbContext, IMapper mapper)
    {
        _auditTrailDbContext = auditTrailDbContext;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<AuditTrailDto>> Handle(GetDirectoryByCreatedDateRangeQuery request, CancellationToken cancellationToken)
    {
        var auditTrailList = await _auditTrailDbContext.AuditTrails
            .Where(a => a.CreatedDate.Date >= request.StartDate.Date && a.CreatedDate.Date <= request.EndDate.Date)
            .ToListAsync(cancellationToken);
        
        var result =
            _mapper.Map<IReadOnlyCollection<Entities.AuditTrail>, IReadOnlyCollection<AuditTrailDto>>(auditTrailList);

        return result;
    }
}