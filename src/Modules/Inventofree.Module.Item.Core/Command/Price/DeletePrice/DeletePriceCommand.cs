using MediatR;

namespace Inventofree.Module.Item.Core.Command.Price.DeletePrice;

public class DeletePriceCommand : IRequest<Unit>
{
    public long Id { get; init; }
}