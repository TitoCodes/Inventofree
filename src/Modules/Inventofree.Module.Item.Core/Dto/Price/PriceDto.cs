using Inventofree.Module.Item.Core.Enums;

namespace Inventofree.Module.Item.Core.Dto.Price
{
    public class PriceDto
    {
        public double Amount  { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}