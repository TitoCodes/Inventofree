using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.Item.Core.Dto.Price;
using Inventofree.Module.Item.Core.Enums;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.AddItem;

public class AddItemCommandValidatorTest
{
    [Fact]
    public void ShouldValidateAddItem()
    {
        //Arrange
        var validator = new AddItemCommandValidator();
        //Act
        var result = validator.Validate(new AddItemCommand() { CreatedBy = 1, Name = "Name", Detail = "Details", CategoryId = 2, Price = new PriceDto(){ Amount = 1, CurrencyType = CurrencyType.Php } });
        //Assert
        result.ShouldNotBeNull();
    }
}