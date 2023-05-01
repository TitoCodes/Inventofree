using Inventofree.Module.Item.Core.Dto.Item;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Dto.Item;

public class ItemPriceDtoTest
{
    [Fact]
    public void ShouldMatchItemPriceDto()
    {
        // Arrange
        var itemPriceDto = new ItemPriceDto
        {
            Amount = 10000
        };
        // Act
        // Assert
        itemPriceDto.Amount.ShouldBe(10000);
    }
}