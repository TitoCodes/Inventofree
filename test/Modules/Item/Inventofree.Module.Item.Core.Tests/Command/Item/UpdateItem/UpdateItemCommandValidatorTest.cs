using Inventofree.Module.Item.Core.Command.Item.UpdateItem;
using Inventofree.Module.Item.Core.Dto.Price;
using Inventofree.Module.Item.Core.Enums;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.UpdateItem;

public class UpdateItemCommandValidatorTest
{
    [Fact]
    public void ShouldValidateUpdateItem()
    {
        //Arrange
        var validator = new UpdateItemCommandValidator();
        //Act
        var result = validator.Validate(new UpdateItemCommand(){ Id = 1, UpdatedBy = 1, Price = new PriceDto(){ Amount = 1, CurrencyType = CurrencyType.Php }, Detail = "Detail", Name = "Name"});
        //Assert
        result.ShouldNotBeNull();
    }
}