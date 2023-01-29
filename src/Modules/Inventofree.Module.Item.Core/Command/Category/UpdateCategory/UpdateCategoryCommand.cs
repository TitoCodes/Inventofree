using MediatR;

namespace Inventofree.Module.Item.Core.Command.Category.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long UpdatedBy { get; set; }
    }
}