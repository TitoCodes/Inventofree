using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Category.AddCategory;

public class AddCategoryCommandValidatorTest
{
    [Fact]
    public void ShouldValidateAddCategory()
    {
        //Arrange
        var validator = new AddCategoryCommandValidator();
        //Act
        var result = validator.Validate(new AddCategoryCommand(){ Name = "Name", Description = "Description", CreatedBy = 1 });
        //Assert
        result.ShouldNotBeNull();
    }
}