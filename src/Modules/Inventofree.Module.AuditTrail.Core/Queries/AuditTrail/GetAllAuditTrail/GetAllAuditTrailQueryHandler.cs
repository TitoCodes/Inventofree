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
        IQueryable<Entities.AuditTrail> auditTrailList;
        if (!string.IsNullOrEmpty(request.SearchString))
            auditTrailList = _context.AuditTrails
                .Where(a=> a.Action != null && a.Action.Contains(request.SearchString)
                           || a.Details != null && a.Details.Contains(request.SearchString));
        else
            auditTrailList = _context.AuditTrails;
        
        if (!auditTrailList.Any()) 
            throw new ArgumentNullException(AuditTrailErrorMessages.AuditTrailListNull);
        
        var result = await auditTrailList
            .OrderBy(a => a.Id)
            .Select(a => _mapper.Map<AuditTrailDto>(a))
            .ToListAsync(cancellationToken);
        
        return result;
    }
}