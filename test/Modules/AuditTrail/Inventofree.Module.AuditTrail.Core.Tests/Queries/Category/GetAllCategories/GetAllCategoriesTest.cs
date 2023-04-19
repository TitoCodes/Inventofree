using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Queries.Category.GetAllCategories;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Queries.Category.GetAllCategories;

public class GetAllCategoriesTest
{
    [Fact]
    public void ShouldReturnCategories()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var expectedCategoriesDto = new List<Item.Core.Entities.Category>() { new() { Id = 1, Name = "Name"} };
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Item.Core.Entities.Category>() { new () { Id = 1, Name = "Name"} });
        var handler = new GetAllCategoriesQueryHandler(itemDbContextMock.Object);
        //Act
        var result = handler.Handle(new GetAllCategoriesQuery() { },
            new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.Result.ShouldBeEquivalentTo(expectedCategoriesDto);
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Item.Core.Entities.Category>());
        var handler = new GetAllCategoriesQueryHandler(itemDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetAllCategoriesQuery() { },
            new CancellationToken(false)));
    }
}