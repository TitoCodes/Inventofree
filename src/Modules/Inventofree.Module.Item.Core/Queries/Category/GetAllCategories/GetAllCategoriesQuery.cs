using System.Collections.Generic;
using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Category.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Entities.Category>>
    {
        
    }
}