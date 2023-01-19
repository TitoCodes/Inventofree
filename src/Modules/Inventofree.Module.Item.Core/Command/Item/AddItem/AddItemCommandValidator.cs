using FluentValidation;

namespace Inventofree.Module.Item.Core.Command.Item.AddItem
{
    public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
    {
        public AddItemCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Detail).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty().NotEqual(0);
        }
    }
}