using Inventofree.Module.Transaction.Core.Enums;
using Inventofree.Shared.Core.Entities;

namespace Inventofree.Module.Transaction.Core.Entities;

public class Transaction: BaseEntity
{
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    public long CreatedBy { get; set; }
    public long ItemId { get; set; }
}