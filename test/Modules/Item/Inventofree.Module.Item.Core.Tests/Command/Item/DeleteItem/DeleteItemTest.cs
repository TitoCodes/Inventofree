using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Item.DeleteItem;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.DeleteItem;

public class DeleteItemTest
{
    [Fact]
    public void ShouldDeleteItem()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var mediatorMock = new Mock<IMediator>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new() { Id = 1 , Name = "Name", Price = new Core.Entities.Price(){ Id = 1, Amount = 20000}} });
        itemDbContextMock
            .Setup(a => a.Prices.Remove(It.IsAny<Core.Entities.Price>()));
        mediatorMock
            .Setup(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()));
        var handler = new DeleteItemCommandHandler(itemDbContextMock.Object, mediatorMock.Object);
        //Act
        var result = handler.Handle(new DeleteItemCommand() { Id = 1 , UserId = 1},
            new CancellationToken(false));
        //Assert
        itemDbContextMock.Verify(a => a.Items.Remove(It.IsAny<Core.Entities.Item>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var mediatorMock = new Mock<IMediator>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new() { Id = 1 , Name = "Name"} });
        mediatorMock
            .Setup(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()));
        var handler = new DeleteItemCommandHandler(itemDbContextMock.Object, mediatorMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new DeleteItemCommand() { Id = 4},
            new CancellationToken(false)));
    }
}