using MediatR;

namespace Inventofree.Module.Item.Core.Command.Item.DeductItem;

public class DeductItemCommand: IRequest<Unit>
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public long Quantity { get; set; }
}