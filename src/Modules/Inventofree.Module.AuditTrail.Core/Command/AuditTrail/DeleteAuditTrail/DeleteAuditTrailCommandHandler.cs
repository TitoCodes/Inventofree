using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.DeleteAuditTrail;

public class DeleteAuditTrailCommandHandler : IRequestHandler<DeleteAuditTrailCommand, Unit>
{
    private readonly IAuditTrailDbContext _auditTrailDbContext;

    public DeleteAuditTrailCommandHandler(IAuditTrailDbContext auditTrailDbContext)
    {
        _auditTrailDbContext = auditTrailDbContext;
    }
    
    public async Task<Unit> Handle(DeleteAuditTrailCommand request, CancellationToken cancellationToken)
    {
        var existingItem =
            await _auditTrailDbContext
                .AuditTrails
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (existingItem == null)
            throw new Exception(string.Format(AuditTrailErrorMessages.AuditTrailNotFound, nameof(Entities.AuditTrail)));

        _auditTrailDbContext.AuditTrails.Remove(existingItem);
        await _auditTrailDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}