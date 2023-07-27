using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Category.DeleteCategory;
using Inventofree.Module.Item.Core.Enums;
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
        categoryDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>()
            {
                new()
                {
                    Id = 1, 
                    Name = "Name", 
                    Detail = "Details", 
                    CreatedBy = 1, 
                    UpdatedBy = 1,
                    CategoryId = 3,
                    CreatedDate = DateTimeOffset.Now, 
                    ModifiedDate = DateTimeOffset.Now,
                    Price = new Core.Entities.Price()
                    {
                        Amount = 1,
                        CreatedDate = DateTimeOffset.Now,
                        Currency = CurrencyType.Php
                    }
                }
            });
        var handler = new DeleteCategoryCommandHandler(categoryDbContextMock.Object);
        //Act
        var result = handler.Handle(new DeleteCategoryCommand() { Id = 1  },
            new CancellationToken(false));
        //Assert
        categoryDbContextMock.Verify(a => a.Categories.Remove(It.IsAny<Core.Entities.Category>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationException()
    {
        //Arrange
        var categoryDbContextMock = new Mock<IItemDbContext>();
        categoryDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 4 , Name = "Name" } });
        categoryDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>()
            {
                new()
                {
                    Id = 1, 
                    Name = "Name", 
                    Detail = "Details", 
                    CreatedBy = 1, 
                    UpdatedBy = 1,
                    CategoryId = 4,
                    CreatedDate = DateTimeOffset.Now, 
                    ModifiedDate = DateTimeOffset.Now,
                    Price = new Core.Entities.Price()
                    {
                        Amount = 1,
                        CreatedDate = DateTimeOffset.Now,
                        Currency = CurrencyType.Php
                    }
                }
            });
        var handler = new DeleteCategoryCommandHandler(categoryDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new DeleteCategoryCommand() { Id = 4},
            new CancellationToken(false)));
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