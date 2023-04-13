using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAuditTrailById;

public class GetAuditTrailByIdHandler : IRequestHandler<GetAuditTrailByIdQuery, AuditTrailDto>
{
    private readonly IAuditTrailDbContext _context;
    private readonly IMapper _mapper;

    public GetAuditTrailByIdHandler(IAuditTrailDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuditTrailDto> Handle(GetAuditTrailByIdQuery request, CancellationToken cancellationToken)
    {
        var auditTrail = await _context.AuditTrails
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (auditTrail == null) 
            throw new NullReferenceException(AuditTrailErrorMessages.AuditTrailNotFound);
            
        var result = _mapper.Map<AuditTrailDto>(auditTrail);
        return result;
    }
}