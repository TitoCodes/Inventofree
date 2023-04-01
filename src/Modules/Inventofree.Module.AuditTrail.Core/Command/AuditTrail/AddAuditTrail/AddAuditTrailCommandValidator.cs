using FluentValidation;

namespace Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;

public class AddAuditTrailCommandValidator: AbstractValidator<AddAuditTrailCommand>
{
    public AddAuditTrailCommandValidator()
    {
        RuleFor(x => x.Action).NotEmpty();
        RuleFor(x => x.Details).NotEmpty();
        RuleFor(x => x.CreatedBy).NotEmpty().NotEqual(0);
    }
}