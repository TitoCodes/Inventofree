using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Category.DeleteCategory;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Category.DeleteCategory;

public class DeleteCategoryTest
{
    [Fact]
    public void ShouldDeleteCategory()
    {
        //Arrange
        var categoryDbContextMock = new Mock<IItemDbContext>();
        categoryDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1 , Name = "Name"} });
        var handler = new DeleteCategoryCommandHandler(categoryDbContextMock.Object);
        //Act
        var result = handler.Handle(new DeleteCategoryCommand() { Id = 1  },
            new CancellationToken(false));
        //Assert
        categoryDbContextMock.Verify(a => a.Categories.Remove(It.IsAny<Core.Entities.Category>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowCategoryArgumentNullException()
    {
        //Arrange
        var categoryDbContextMock = new Mock<IItemDbContext>();
        categoryDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1 , Name = "Name"} });
        var handler = new DeleteCategoryCommandHandler(categoryDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new DeleteCategoryCommand() { Id = 4},
            new CancellationToken(false)));
    }
}