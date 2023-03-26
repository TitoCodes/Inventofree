using Inventofree.Module.Item.Core.Enums;
using Inventofree.Shared.Core.Entities;

namespace Inventofree.Module.Item.Core.Entities
{
    public class Price : BaseEntity
    {
        public CurrencyType Currency { get; set; }
        public double Amount { get; set; }
    }
}