using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Queries.Item.GetItemsCount;

public class GetItemsCountQueryHandler: IRequestHandler<GetItemsCountQuery, long>
{
    private readonly IItemDbContext _context;

    public GetItemsCountQueryHandler(IItemDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(GetItemsCountQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _context.Items.SumAsync(a => a.Quantity, cancellationToken);
        return totalCount ?? 0;
    }
}