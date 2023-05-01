using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Entities;

public class CategoryTest
{
    [Fact]
    public void ShouldMatchEntity()
    {
        //Arrange
        var dateCreated = DateTimeOffset.UtcNow;
        var existingItems = new List<Core.Entities.Item>() { new Core.Entities.Item() { Id = 1 } };
        var entity = new Core.Entities.Category()
        {
            Id = 1,
            CreatedDate = dateCreated,
            ModifiedDate = dateCreated,
            Name = "Name",
            Description = "Description",
            CreatedBy = 1,
            UpdatedBy = 1,
            Items = existingItems
        };
        //Act
        //Assert
        entity.Id.ShouldBeEquivalentTo((long)1);
        entity.UpdatedBy.ShouldBeEquivalentTo((long)1);
        entity.CreatedBy.ShouldBeEquivalentTo((long)1);
        entity.CreatedDate.ShouldBeEquivalentTo(dateCreated);
        entity.ModifiedDate.ShouldBeEquivalentTo(dateCreated);
        entity.Description.ShouldBeEquivalentTo("Description");
        entity.Name.ShouldBeEquivalentTo("Name");
        entity.Items.ShouldBeEquivalentTo(existingItems);
    }
}