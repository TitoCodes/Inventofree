using MediatR;

namespace Inventofree.Module.Transaction.Core.Command.Transaction.AddTransaction;

public class AddTransactionCommand: IRequest<Unit>
{
    public long CreatedBy { get; set; }
    public long ItemId { get; set; }
    public double Amount { get; set; }
}