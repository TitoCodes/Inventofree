using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;

public class UpdateAuditTrailCommandHandler: IRequestHandler<UpdateAuditTrailCommand, Unit>
{
    private readonly IAuditTrailDbContext _auditTrailDbContext;
    private readonly IUserDbContext _userDbContext;

    public UpdateAuditTrailCommandHandler(IAuditTrailDbContext auditTrailDbContext, IUserDbContext userDbContext)
    {
        _auditTrailDbContext = auditTrailDbContext;
        _userDbContext = userDbContext;
    }
    
    public async Task<Unit> Handle(UpdateAuditTrailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == request.UpdatedBy, cancellationToken);

        if (user == null)
            throw new Exception(UserErrorMessages.UserNotFound);

        var existingAuditTrail =
            await _auditTrailDbContext.AuditTrails.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (existingAuditTrail == null)
            throw new Exception(AuditTrailErrorMessages.AuditTrailNotFound);
        
        existingAuditTrail.ModifiedDate = DateTimeOffset.UtcNow;
        existingAuditTrail.UpdatedBy = request.UpdatedBy;
        existingAuditTrail.Action = request.Action;
        existingAuditTrail.Details = request.Details;

        await _auditTrailDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}