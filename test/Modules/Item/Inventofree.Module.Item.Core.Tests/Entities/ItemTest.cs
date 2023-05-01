using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Entities;

public class ItemTest
{
    [Fact]
    public void ShouldMatchEntity()
    {
        //Arrange
        var dateCreated = DateTimeOffset.UtcNow;
        var existingCategory = new Core.Entities.Category(){ Id = 1 };
        var existingPrice = new Core.Entities.Price() { Amount = 1 };
        var entity = new Core.Entities.Item()
        {
            Id = 1,
            CreatedDate = dateCreated,
            ModifiedDate = dateCreated,
            Name = "Name",
            Detail = "Detail",
            CreatedBy = 1,
            UpdatedBy = 1,
            CategoryId = 1,
            PriceId = 1,
            Category = existingCategory,
            Price = existingPrice
        };
        //Act
        //Assert
        entity.Id.ShouldBeEquivalentTo((long)1);
        entity.UpdatedBy.ShouldBeEquivalentTo((long)1);
        entity.CategoryId.ShouldBeEquivalentTo((long)1);
        entity.PriceId.ShouldBeEquivalentTo((long)1);
        entity.CreatedBy.ShouldBeEquivalentTo((long)1);
        entity.CreatedDate.ShouldBeEquivalentTo(dateCreated);
        entity.ModifiedDate.ShouldBeEquivalentTo(dateCreated);
        entity.Detail.ShouldBeEquivalentTo("Detail");
        entity.Name.ShouldBeEquivalentTo("Name");
        entity.Category.ShouldBeEquivalentTo(existingCategory);
        entity.Price.ShouldBeEquivalentTo(existingPrice);
    }
}