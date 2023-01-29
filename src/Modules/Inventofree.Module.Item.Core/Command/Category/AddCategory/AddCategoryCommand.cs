using MediatR;

namespace Inventofree.Module.Item.Core.Command.Category.AddCategory
{
    public class AddCategoryCommand: IRequest<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
    }
}