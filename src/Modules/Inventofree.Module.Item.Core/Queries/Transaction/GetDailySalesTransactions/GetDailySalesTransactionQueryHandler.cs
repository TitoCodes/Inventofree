using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using MediatR;

namespace Inventofree.Module.Item.Core.Queries.Transaction.GetDailySalesTransactions;

public class GetDailySalesTransactionQueryHandler: IRequestHandler<GetDailySalesTransactionQuery, double>
{
    private readonly IItemDbContext _context;

    public GetDailySalesTransactionQueryHandler(IItemDbContext context)
    {
        _context = context;
    }

    public async Task<double> Handle(GetDailySalesTransactionQuery request, CancellationToken cancellationToken)
    {
        return 6;
    }
}