using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Dto.Item;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Queries.Item.GetItemById
{
    public class GetItemByIdQueryHandler: IRequestHandler<GetItemByIdQuery, ItemDto>
    {
        private readonly IItemDbContext _context;
        private readonly IMapper _mapper;

        public GetItemByIdQueryHandler(IItemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Items
                .Include(a => a.Category)
                .Include(a => a.Price)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
            if (item == null) 
                throw new NullReferenceException(ItemErrorMessages.NotFound);
            
            var result = _mapper.Map<ItemDto>(item);
            return result;
        }
    }
}