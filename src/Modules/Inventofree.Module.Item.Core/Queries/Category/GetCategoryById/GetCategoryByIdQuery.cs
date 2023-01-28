using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Category.GetCategoryById
{
    public class GetCategoryByIdQuery: IRequest<Entities.Category>
    {
        public int Id { get; set; }
    }
}