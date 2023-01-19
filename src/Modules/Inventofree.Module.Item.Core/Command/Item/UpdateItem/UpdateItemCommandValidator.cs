using FluentValidation;

namespace Inventofree.Module.Item.Core.Command.Item.UpdateItem
{
    public class UpdateItemCommandValidator: AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Detail).NotEmpty();
            RuleFor(x => x.UpdatedBy).NotEmpty();
        }
    }
}