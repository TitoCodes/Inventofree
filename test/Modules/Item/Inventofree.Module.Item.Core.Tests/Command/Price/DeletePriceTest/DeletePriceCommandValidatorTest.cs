using Inventofree.Module.Item.Core.Command.Price.DeletePrice;
using Xunit;
using Shouldly;

namespace Inventofree.Module.Item.Core.Tests.Command.Price.DeletePriceTest;

public class DeletePriceCommandValidatorTest
{
    [Fact]
    public void ShouldValidateDeleteCategory()
    {
        //Arrange
        var validator = new DeletePriceCommandValidator();
        //Act
        var result = validator.Validate(new DeletePriceCommand(){ Id = 1});
        //Assert
        result.ShouldNotBeNull();
    }
}