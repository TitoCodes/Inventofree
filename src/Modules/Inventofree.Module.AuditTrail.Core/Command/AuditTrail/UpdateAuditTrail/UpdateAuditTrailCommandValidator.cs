using FluentValidation;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;

public class UpdateAuditTrailCommandValidator: AbstractValidator<UpdateAuditTrailCommand>
{
    public UpdateAuditTrailCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        RuleFor(x => x.Action).NotEmpty();
        RuleFor(x => x.Details).NotEmpty();
        RuleFor(x => x.UpdatedBy).NotEmpty();
    }
}