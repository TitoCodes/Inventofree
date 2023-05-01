using Inventofree.Module.Item.Core.Command.Category.DeleteCategory;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Category.DeleteCategory;

public class DeleteCategoryCommandValidatorTest
{
    [Fact]
    public void ShouldValidateDeleteCategory()
    {
        //Arrange
        var validator = new DeleteCategoryValidator();
        //Act
        var result = validator.Validate(new DeleteCategoryCommand(){ Id = 1});
        //Assert
        result.ShouldNotBeNull();
    }
}