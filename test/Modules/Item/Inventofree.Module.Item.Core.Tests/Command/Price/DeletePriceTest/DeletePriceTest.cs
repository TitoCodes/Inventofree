using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Price.DeletePrice;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Price.DeletePriceTest;

public class DeletePriceTest
{
    [Fact]
    public void ShouldDeletePrice()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        itemDbContextMock
            .Setup(a => a.Prices)
            .ReturnsDbSet(new List<Core.Entities.Price>() { new() { Id = 1 } });
        var handler = new DeletePriceCommandHandler(itemDbContextMock.Object);
        //Act
        var result = handler.Handle(new DeletePriceCommand() { Id = 1  },
            new CancellationToken(false));
        //Assert
        itemDbContextMock.Verify(a => a.Prices.Remove(It.IsAny<Core.Entities.Price>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        itemDbContextMock
            .Setup(a => a.Prices)
            .ReturnsDbSet(new List<Core.Entities.Price>() { new() { Id = 1 } });
        var handler = new DeletePriceCommandHandler(itemDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new DeletePriceCommand() { Id = 4},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        itemDbContextMock
            .Setup(a => a.Prices)
            .ReturnsDbSet(new List<Core.Entities.Price>() { new() { Id = 1 } });
        var handler = new DeletePriceCommandHandler(itemDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(null,
            new CancellationToken(false)));
    }
}