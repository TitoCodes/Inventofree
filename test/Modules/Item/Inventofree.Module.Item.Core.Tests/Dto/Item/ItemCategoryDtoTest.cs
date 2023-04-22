using Inventofree.Module.Item.Core.Dto.Item;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Dto.Item;

public class ItemCategoryDtoTest
{
    [Fact]
    public void ShouldMatchItemCategoryDto()
    {
        // Arrange
        var itemCategoryDto = new ItemCategoryDto
        {
            Name = "Name",
            Description = "Description"
        };
        // Act
        // Assert
        itemCategoryDto.Name.ShouldBe("Name");
        itemCategoryDto.Description.ShouldBe("Description");
    }
}