using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Queries.Category.GetCategoryById
{
    public class GetCategoryByIdQueryHandler: IRequestHandler<GetCategoryByIdQuery, Entities.Category>
    {
        private readonly IItemDbContext _context;

        public GetCategoryByIdQueryHandler(IItemDbContext context)
        {
            _context = context;
        }

        public async Task<Entities.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
            if (category == null) 
                throw new ArgumentNullException(string.Format(ItemErrorMessages.NotFound, nameof(Entities.Category)));
            
            return category;
        }
    }
}