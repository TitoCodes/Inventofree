using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Price.DeletePrice;

public class DeletePriceCommandHandler: IRequestHandler<DeletePriceCommand, Unit>
{
    private readonly IItemDbContext _itemDbContext;

    public DeletePriceCommandHandler(IItemDbContext itemDbContext)
    {
        _itemDbContext = itemDbContext;
    }
    
    public async Task<Unit> Handle(DeletePriceCommand request, CancellationToken cancellationToken)
    {
        if(request == null)
            throw new System.ArgumentNullException(nameof(request));
        
        var price = await _itemDbContext.Prices.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        
        if(price == null)
            throw new System.InvalidOperationException("Price not found");
        _itemDbContext.Prices.Remove(price);
        
        await _itemDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}