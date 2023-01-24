using MediatR;

namespace Inventofree.Module.Item.Core.Command.Category.DeleteCategory
{
    public class DeleteCategoryCommand: IRequest<Unit>
    {
        public int Id { get; set; }
    }
}