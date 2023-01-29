using Inventofree.Module.Item.Core.Dto.Item;
using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Item.GetItemById
{
    public class GetItemByIdQuery: IRequest<ItemDto>
    {
        public int Id { get; set; }
    }
}