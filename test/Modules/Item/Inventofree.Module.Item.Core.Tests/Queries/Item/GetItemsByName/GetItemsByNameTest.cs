using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Dto.Item;
using Inventofree.Module.Item.Core.Queries.Item.GetItemById;
using Inventofree.Module.Item.Core.Queries.Item.GetItemsByName;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Queries.Item.GetItemsByName;

public class GetItemsByNameTest
{
    [Fact]
    public async Task ShouldReturnItem()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var itemDbContextMock = new Mock<IItemDbContext>();
        var expectedItemDto = new List<ItemDto>() { new() { Id = 1, Name = "Name"} };
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new () { Id = 1, Name = "Name", UpdatedBy = 1, CreatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        mapperMock.Setup(a => a.Map<ItemDto>(It.IsAny<Core.Entities.Item>()))
            .Returns(new ItemDto() { Id = 1, Name = "Name"});
        var handler = new GetItemsByNameQueryHandler(itemDbContextMock.Object, mapperMock.Object);
        //Act
        var result = await handler.Handle(new GetItemsByNameQuery() { Name = "Name"  },
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
        var handler = new GetItemsByNameQueryHandler(itemContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetItemsByNameQuery() { Name = "Name"},
            new CancellationToken(false)));
    }
}