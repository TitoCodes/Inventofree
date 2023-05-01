using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.Item.Core.Dto.Price;
using Inventofree.Module.Item.Core.Enums;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.AddItem;

public class AddItemCommandTest
{
    //add test here
    [Fact]
    public void ShouldEqualCommand()
    {
        //Arrange
        var command = new AddItemCommand()
        {
            CreatedBy = 1,
            Name = "Name",
            Detail = "Details",
            CategoryId = 2,
            Price = new PriceDto()
            {
                Amount = 1,
                CurrencyType = CurrencyType.Php
            }
        };
        //Act
        //Assert
        command.Detail.ShouldBe("Details");
        command.Price.Amount.ShouldBe(1);
        command.CategoryId.Value.ShouldBe(2);
        command.Name.ShouldBe("Name");
        command.CreatedBy.ShouldBe(1);
    }
}