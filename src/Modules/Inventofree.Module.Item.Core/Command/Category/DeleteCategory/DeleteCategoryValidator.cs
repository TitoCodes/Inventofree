using FluentValidation;

namespace Inventofree.Module.Item.Core.Command.Category.DeleteCategory
{
    public class DeleteCategoryValidator: AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotEqual(0);
        }
    }
}