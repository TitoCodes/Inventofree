using FluentValidation;

namespace Inventofree.Module.Item.Core.Command.Category.AddCategory
{
    public class AddCategoryCommandValidator: AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty().NotEqual(0);
        }
    }
}