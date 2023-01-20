using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Queries.Item.GetItemById
{
    public class GetItemByIdQueryHandler: IRequestHandler<GetItemByIdQuery, Entities.Item>
    {
        private readonly IItemDbContext _context;

        public GetItemByIdQueryHandler(IItemDbContext context)
        {
            _context = context;
        }

        public async Task<Entities.Item> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
            if (item == null) 
                throw new Exception(ItemErrorMessages.NotFound);
            return item;
        }
    }
}