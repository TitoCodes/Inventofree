using Inventofree.Module.Item.Core.Dto.Item;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Dto.Item;

public class ItemDtoTest
{
    [Fact]
    public void ShouldMatchItemDto()
    {
        // Arrange
        var createdDate = DateTimeOffset.UtcNow;
        var modifiedDate = DateTimeOffset.UtcNow;
        var priceDto = new ItemPriceDto()
        {
            Amount = 1000
        };
        var categoryDto = new ItemCategoryDto()
        {
            Description = "Test Category",
            Name = "Test"
        };
        var itemDto = new ItemDto()
        {
            Id = 1,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            Name = "Test",
            Detail = "Details",
            CreatedBy = 1,
            UpdatedBy = 1,
            CategoryId = 1,
            PriceId = 1,
            Price = priceDto,
            Category = categoryDto
        };
        // Act
        // Assert
        itemDto.Id.ShouldBe(1);
        itemDto.Name.ShouldBe("Test");
        itemDto.Detail.ShouldBe("Details");
        itemDto.CreatedBy.ShouldBe(1);
        itemDto.UpdatedBy.ShouldBe(1);
        itemDto.CategoryId.ShouldBe(1);
        itemDto.PriceId.ShouldBe(1);
        itemDto.Category.ShouldBeEquivalentTo(categoryDto);
        itemDto.Price.ShouldBeEquivalentTo(priceDto);
        itemDto.CreatedDate.ShouldBe(createdDate);
        itemDto.ModifiedDate.ShouldBe(modifiedDate);
    }
}