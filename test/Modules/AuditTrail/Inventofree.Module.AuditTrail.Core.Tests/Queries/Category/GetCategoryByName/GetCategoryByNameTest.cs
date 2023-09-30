using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Queries.Category.GetCategoryById;
using Inventofree.Module.Item.Core.Queries.Category.GetCategoryByName;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Queries.Category.GetCategoryByName;

public class GetCategoryByNameTest
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
        var handler = new GetCategoryByNameQueryHandler(itemDbContextMock.Object);
        //Act
        var result = await handler.Handle(new GetCategoryByNameQuery() { Name = "Name"},
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
        var handler = new GetCategoryByNameQueryHandler(itemDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetCategoryByNameQuery() { Name = "Some Name"},
            new CancellationToken(false)));
    }
}