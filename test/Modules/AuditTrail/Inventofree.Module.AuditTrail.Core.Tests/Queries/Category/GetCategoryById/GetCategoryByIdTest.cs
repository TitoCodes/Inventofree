using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Queries.Category.GetCategoryById;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Queries.Category.GetCategoryById;

public class GetCategoryByIdTest
{
    [Fact]
    public async Task ShouldReturnCategory()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var expectedCategoryDto = new Item.Core.Entities.Category() {  Id = 1, Name = "Name" };
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Item.Core.Entities.Category>() { new () { Id = 1, Name = "Name"} });
        var handler = new GetCategoryByIdQueryHandler(itemDbContextMock.Object);
        //Act
        var result = await handler.Handle(new GetCategoryByIdQuery() { Id = 1},
            new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(expectedCategoryDto);
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Item.Core.Entities.Category>() { new () { Id = 1, Name = "Name"} });
        var handler = new GetCategoryByIdQueryHandler(itemDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetCategoryByIdQuery() { Id = 2 },
            new CancellationToken(false)));
    }
}