using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Queries.Item.GetItemsCount;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Queries.Item.GetItemsCount;

public class GetItemsCountTest
{
    [Fact]
    public async Task ShouldReturnItem()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        long expectedCount = 5;
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new () { Id = 1, Name = "Name", UpdatedBy = 1, CreatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now, Quantity = expectedCount} });
        var handler = new GetItemsCountQueryHandler(itemDbContextMock.Object);
        //Act
        var result = await handler.Handle(new GetItemsCountQuery(), new CancellationToken(false));
        //Assert
        result.ShouldBeEquivalentTo(expectedCount);
    }
}