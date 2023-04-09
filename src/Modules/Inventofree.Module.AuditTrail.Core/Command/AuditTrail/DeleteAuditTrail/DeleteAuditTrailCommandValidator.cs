using FluentValidation;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.DeleteAuditTrail;

public class DeleteAuditTrailCommandValidator: AbstractValidator<DeleteAuditTrailCommand>
{
    public DeleteAuditTrailCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
    }
}