using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAllAuditTrail;

public class GetAllAuditTrailHandler: IRequestHandler<GetAllAuditTrailQuery, IReadOnlyCollection<AuditTrailDto>>
{
    private readonly IAuditTrailDbContext _context;
    private readonly IMapper _mapper;

    public GetAllAuditTrailHandler(IAuditTrailDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<AuditTrailDto>> Handle(GetAllAuditTrailQuery request, CancellationToken cancellationToken)
    {
        var auditTrailList = await _context.AuditTrails
            .OrderBy(a => a.Id)
            .ToListAsync(cancellationToken);
        if (!auditTrailList.Any()) 
            throw new Exception(AuditTrailErrorMessages.AuditTrailListNull);
            
        var result = auditTrailList.Select(a => _mapper.Map<AuditTrailDto>(a)).ToList();
        return result;
    }
}