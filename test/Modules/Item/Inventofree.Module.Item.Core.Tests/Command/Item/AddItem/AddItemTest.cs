using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Shared.Core.Exceptions;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Item.AddItem;

public class AddItemTest
{
    [Fact]
    public void ShouldAddItem()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<AddItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        var handler = new AddItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new AddItemCommand() { CreatedBy = 1, Name = "Names", Detail = "Details", CategoryId = 1},
            new CancellationToken(false));
        //Assert
        itemDbContextMock.Verify(a => a.Items.AddAsync( It.IsAny<Core.Entities.Item>() , It.IsAny<CancellationToken>()), Times.Once);
        itemDbContextMock.Verify(a => a.SaveChangesAsync( It.IsAny<CancellationToken>()), Times.Once);
        result.ShouldNotBeNull();
        result.Result.ShouldBe(1);
    }
    
    [Fact]
    public void ShouldThrowUserNullInvalidOperationException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 2 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<AddItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        var handler = new AddItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new AddItemCommand() { CreatedBy = 1, Name = "Names", Detail = "Details", CategoryId = 1},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowCategoryNullInvalidOperationException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<AddItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        var handler = new AddItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new AddItemCommand() { CreatedBy = 1, Name = "Names", Detail = "Details", CategoryId = 2},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowDuplicateNameException()
    {
        //Arrange
        var itemDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        itemDbContextMock
            .Setup(a => a.Items)
            .ReturnsDbSet(new List<Core.Entities.Item>() { new() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        itemDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Item>(It.IsAny<AddItemCommand>()))
            .Returns(new Core.Entities.Item() { Id = 1, Name = "Name", Detail = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        var handler = new AddItemCommandHandler(itemDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<DuplicateNameException>(() => handler.Handle(new AddItemCommand() { CreatedBy = 1, Name = "Name", Detail = "Details", CategoryId = 2},
            new CancellationToken(false)));
    }
}