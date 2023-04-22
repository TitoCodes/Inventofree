using Inventofree.Module.Item.Core.Command.Item.SetItemCategory;
using Inventofree.Module.Item.Core.Command.Price.DeletePrice;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.SetItemCategory;

public class SetItemCategoryCommandValidatorTest
{
    [Fact]
    public void ShouldValidateSetItemCategory()
    {
        //Arrange
        var validator = new SetItemCategoryCommandValidator();
        //Act
        var result = validator.Validate(new SetItemCategoryCommand(){ UserId = 1, CategoryId = 1, ItemId = 1});
        //Assert
        result.ShouldNotBeNull();
    }
}