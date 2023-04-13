using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Dto.Item;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Queries.Item.GetAllItems
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IReadOnlyCollection<ItemDto>>
    {
        private readonly IItemDbContext _context;
        private readonly IMapper _mapper;

        public GetAllItemsQueryHandler(IItemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<ItemDto>> Handle(GetAllItemsQuery request,
            CancellationToken cancellationToken)
        {
            var items = await _context.Items
                .Include(a => a.Category)
                .Include(a => a.Price)
                .OrderBy(a => a.Id)
                .ToListAsync(cancellationToken);
            if (!items.Any()) 
                throw new NullReferenceException(ItemErrorMessages.ItemsNull);
            
            var result = items.Select(a => _mapper.Map<ItemDto>(a)).ToList();
            return result;
        }
    }
}