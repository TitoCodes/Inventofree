using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;

public class AddAuditTrailCommandHandler: IRequestHandler<AddAuditTrailCommand, long>
{
    private readonly IAuditTrailDbContext _auditTrailDbContext;
    private readonly IUserDbContext _userDbContext;
    private readonly IMapper _mapper;

    public AddAuditTrailCommandHandler(IAuditTrailDbContext auditTrailDbContext, IUserDbContext userDbContext, IMapper mapper)
    {
        _auditTrailDbContext = auditTrailDbContext;
        _userDbContext = userDbContext;
        _mapper = mapper;
    }
    public async Task<long> Handle(AddAuditTrailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDbContext.Users.FirstOrDefaultAsync(a => a.Id == request.CreatedBy,
            cancellationToken);

        if (user == null)
            throw new ArgumentNullException(UserErrorMessages.UserNotFound);

        var auditTrail = _mapper.Map<Entities.AuditTrail>(request);
        await _auditTrailDbContext.AuditTrails.AddAsync(auditTrail, cancellationToken);
        await _auditTrailDbContext.SaveChangesAsync(cancellationToken);
        return auditTrail.Id;
    }
}