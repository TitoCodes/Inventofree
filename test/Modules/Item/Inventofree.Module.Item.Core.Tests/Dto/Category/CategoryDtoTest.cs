using Inventofree.Module.Item.Core.Dto.Category;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Dto.Category;

public class CategoryDtoTest
{
    [Fact]
    public void ShouldMatchCategoryDto()
    {
        // Arrange
        var createdDate = DateTimeOffset.UtcNow;
        var modifiedDate = DateTimeOffset.UtcNow;
        var itemDto = new CategoryDto()
        {
            Id = 1,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            Name = "Test",
            Description = "Description",
            CreatedBy = 1,
            UpdatedBy = 1
        };
        // Act
        // Assert
        itemDto.Id.ShouldBe(1);
        itemDto.Name.ShouldBe("Test");
        itemDto.Description.ShouldBe("Description");
        itemDto.CreatedBy.ShouldBe(1);
        itemDto.UpdatedBy.ShouldBe(1);
        itemDto.CreatedDate.ShouldBe(createdDate);
        itemDto.ModifiedDate.ShouldBe(modifiedDate);
    }
}