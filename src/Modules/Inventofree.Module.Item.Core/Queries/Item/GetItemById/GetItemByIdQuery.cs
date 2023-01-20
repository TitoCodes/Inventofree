using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Item.GetItemById
{
    public class GetItemByIdQuery: IRequest<Entities.Item>
    {
        public int Id { get; set; }
    }
}