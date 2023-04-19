using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Queries.Category.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Entities.Category>>
    {
        private readonly IItemDbContext _context;

        public GetAllCategoriesQueryHandler(IItemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities.Category>> Handle(GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.OrderBy(a => a.Id).ToListAsync(cancellationToken);
            if (!categories.Any()) 
                throw new ArgumentNullException(ItemErrorMessages.CategoriesNotFound);
            
            return categories;
        }
    }
}