using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Dto.Item;
using Inventofree.Module.Item.Core.Queries.Item.GetAllItems;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Queries.Item.GetAllItems;

public class GetAllItemsQueryTest
{
    [Fact]
    public async Task ShouldReturnItems()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var itemContextMock = new Mock<IItemDbContext>();
        var expectedItemDto = new List<ItemDto>() { new ItemDto() { Id = 1 } };
        itemContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new () { Id = 1, Name = "Name" , Detail  = "Details", UpdatedBy = 1, CreatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        mapperMock.Setup(a => a.Map<ItemDto>(It.IsAny<Core.Entities.Item>()))
            .Returns(new ItemDto() { Id = 1 });
        var handler = new GetAllItemsQueryHandler(itemContextMock.Object, mapperMock.Object);
        //Act
        var result = await handler.Handle(new GetAllItemsQuery() { },
            new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(expectedItemDto);
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var itemContextMock = new Mock<IItemDbContext>();
        itemContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>());
        mapperMock.Setup(a => a.Map<ItemDto>(It.IsAny<Core.Entities.Item>()))
            .Returns(new ItemDto() { Id = 1 });
        var handler = new GetAllItemsQueryHandler(itemContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetAllItemsQuery() { },
            new CancellationToken(false)));
    }
}