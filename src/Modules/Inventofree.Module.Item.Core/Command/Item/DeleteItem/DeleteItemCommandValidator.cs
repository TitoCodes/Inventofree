using FluentValidation;

namespace Inventofree.Module.Item.Core.Command.Item.DeleteItem
{
    public class DeleteItemCommandValidator: AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        }
    }
}