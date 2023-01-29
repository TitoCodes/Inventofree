using FluentValidation;

namespace Inventofree.Module.Item.Core.Command.Item.SetItemCategory
{
    public class SetItemCategoryCommandValidator : AbstractValidator<SetItemCategoryCommand>
    {
        public SetItemCategoryCommandValidator()
        {
            RuleFor(x => x.ItemId).NotEmpty().NotEqual(0);
            RuleFor(x => x.UserId).NotEmpty().NotEqual(0);
            RuleFor(x => x.CategoryId).NotEmpty().NotEqual(0);
        }
    }
}