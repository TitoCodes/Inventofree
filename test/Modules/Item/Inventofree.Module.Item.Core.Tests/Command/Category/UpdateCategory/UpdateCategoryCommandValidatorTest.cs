using Inventofree.Module.Item.Core.Command.Category.UpdateCategory;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Category.UpdateCategory;

public class UpdateCategoryCommandValidatorTest
{
    [Fact]
    public void ShouldValidateUpdateCategory()
    {
        //Arrange
        var validator = new UpdateCategoryCommandValidator();
        //Act
        var result = validator.Validate(new UpdateCategoryCommand(){ Id = 1, Name = "Name", Description = "Description", UpdatedBy = 1});
        //Assert
        result.ShouldNotBeNull();
    }
}