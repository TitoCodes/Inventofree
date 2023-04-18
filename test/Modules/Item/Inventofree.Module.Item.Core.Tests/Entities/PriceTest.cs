using Inventofree.Module.Item.Core.Enums;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Entities;

public class PriceTest
{
    [Fact]
    public void ShouldMatchEntity()
    {
        //Arrange
        var dateCreated = DateTimeOffset.UtcNow;
        var entity = new Core.Entities.Price()
        {
            Id = 1,
            CreatedDate = dateCreated,
            ModifiedDate = dateCreated,
            Amount = 10,
            Currency = CurrencyType.Php
        };
        //Act
        //Assert
        entity.Id.ShouldBeEquivalentTo((long)1);
        entity.Amount.ShouldBeEquivalentTo((double)10);
        entity.Currency.ShouldBeEquivalentTo(CurrencyType.Php);
        entity.CreatedDate.ShouldBeEquivalentTo(dateCreated);
        entity.ModifiedDate.ShouldBeEquivalentTo(dateCreated);
    }
}