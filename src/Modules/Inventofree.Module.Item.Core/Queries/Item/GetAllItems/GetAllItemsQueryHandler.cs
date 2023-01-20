using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Queries.Item.GetAllItems
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<Entities.Item>>
    {
        private readonly IItemDbContext _context;

        public GetAllItemsQueryHandler(IItemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities.Item>> Handle(GetAllItemsQuery request,
            CancellationToken cancellationToken)
        {
            var items = await _context.Items.OrderBy(a => a.Id).ToListAsync(cancellationToken);
            if (items == null) 
                throw new Exception(ItemErrorMessages.ItemsNull);
            return items;
        }
    }
}