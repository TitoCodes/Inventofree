using System.Collections.Generic;
using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Item.GetAllItems
{
    public class GetAllItemsQuery : IRequest<IEnumerable<Entities.Item>>
    {
        
    }
}