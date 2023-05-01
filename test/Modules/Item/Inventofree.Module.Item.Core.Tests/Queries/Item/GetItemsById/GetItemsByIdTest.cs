using AutoMapper;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAuditTrailById;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Dto.Item;
using Inventofree.Module.Item.Core.Queries.Item.GetItemById;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Queries.Item.GetItemsById;

public class GetItemsByIdTest
{
    [Fact]
    public void ShouldReturnItem()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var itemDbContextMock = new Mock<IItemDbContext>();
        var expectedItemDto =  new ItemDto() { Id = 1};
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new () { Id = 1, UpdatedBy = 1, CreatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        mapperMock.Setup(a => a.Map<ItemDto>(It.IsAny<Core.Entities.Item>()))
            .Returns(new ItemDto() { Id = 1 });
        var handler = new GetItemByIdQueryHandler(itemDbContextMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new GetItemByIdQuery() { Id = 1  },
            new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.Result.ShouldBeEquivalentTo(expectedItemDto);
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var itemDbContextMock = new Mock<IItemDbContext>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new () { Id = 2 } });
        var handler = new GetItemByIdQueryHandler(itemDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetItemByIdQuery() { Id = 1 },
            new CancellationToken(false)));
    }
}