using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Category.GetCategoryByName
{
    public class GetCategoryByNameQuery: IRequest<Entities.Category>
    {
        public string Name { get; set; }
    }
}