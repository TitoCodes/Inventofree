using System.Collections.Generic;
using Inventofree.Module.Item.Core.Dto.Item;
using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Item.GetItemsByName
{
    public class GetItemsByNameQuery: IRequest<IReadOnlyCollection<ItemDto>>
    {
        public string Name { get; set; }
    }
}