using FluentValidation;

namespace Inventofree.Module.Item.Core.Command.Price.DeletePrice;

public class DeletePriceCommandValidator: AbstractValidator<DeletePriceCommand>
{
    public DeletePriceCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}