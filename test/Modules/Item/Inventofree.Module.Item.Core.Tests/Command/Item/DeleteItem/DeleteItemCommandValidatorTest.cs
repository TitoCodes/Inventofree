using Inventofree.Module.Item.Core.Command.Item.DeleteItem;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.DeleteItem;

public class DeleteItemCommandValidatorTest
{
    [Fact]
    public void ShouldValidateDeleteItem()
    {
        //Arrange
        var validator = new DeleteItemCommandValidator();
        //Act
        var result = validator.Validate(new DeleteItemCommand(){ Id = 1, UserId = 1});
        //Assert
        result.ShouldNotBeNull();
    }
}