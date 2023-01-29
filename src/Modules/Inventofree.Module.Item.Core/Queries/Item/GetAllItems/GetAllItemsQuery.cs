using System.Collections.Generic;
using Inventofree.Module.Item.Core.Dto.Item;
using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Item.GetAllItems
{
    
    public class GetAllItemsQuery : IRequest<IReadOnlyCollection<ItemDto>>
    {
        
    }
}