using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Item.SetItemCategory;
using Inventofree.Module.Item.Core.Dto.Price;
using Inventofree.Module.Item.Core.Enums;
using Inventofree.Module.User.Core.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.SetItemCategory;

public class SetItemCategoryTest
{
    [Fact]
    public void ShouldSetItemCategory()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        itemDbContextMock
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
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new SetItemCategoryCommandHandler(itemDbContextMock.Object, userDbContextMock.Object);
        //Act
        var result = handler.Handle(new SetItemCategoryCommand() { CategoryId = 1, ItemId = 1, UserId = 1},
            new CancellationToken(false));
        //Assert
        itemDbContextMock.Verify(a => a.Items.Update( It.IsAny<Core.Entities.Item>()), Times.Once);
        itemDbContextMock.Verify(a => a.SaveChangesAsync( It.IsAny<CancellationToken>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowUserNullInvalidOperationException()
    {
        //Arrange
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        itemDbContextMock
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
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new SetItemCategoryCommandHandler(itemDbContextMock.Object, userDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new SetItemCategoryCommand() { UserId = 2, ItemId = 1, CategoryId = 1},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowCategoryNullInvalidOperationException()
    {
        //Arrange
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        itemDbContextMock
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
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new SetItemCategoryCommandHandler(itemDbContextMock.Object, userDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new SetItemCategoryCommand() { UserId = 1, ItemId = 1, CategoryId = 2},
            new CancellationToken(false)));
    }
      
    [Fact]
    public void ShouldThrowItemNullInvalidOperationException()
    {
        //Arrange
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        itemDbContextMock
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
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new SetItemCategoryCommandHandler(itemDbContextMock.Object, userDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new SetItemCategoryCommand() { UserId = 1, ItemId = 2, CategoryId = 1},
            new CancellationToken(false)));
    }
}