using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Category.UpdateCategory;
using Inventofree.Module.User.Core.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Category.UpdateCategory;

public class UpdateCategoryTest
{
    [Fact]
    public void ShouldUpdateCategory()
    {
        //Arrange
        var categoryDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        categoryDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Category>(It.IsAny<UpdateCategoryCommand>()))
            .Returns(new Core.Entities.Category() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        var handler = new UpdateCategoryCommandHandler(categoryDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new UpdateCategoryCommand() { Id = 1, UpdatedBy = 1, Name = "Names", Description = "Description" },
            new CancellationToken(false));
        //Assert
        categoryDbContextMock.Verify(a => a.Categories.Update( It.IsAny<Core.Entities.Category>()), Times.Once);
        categoryDbContextMock.Verify(a => a.SaveChangesAsync( It.IsAny<CancellationToken>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowDuplicateException()
    {
        //Arrange
        var categoryDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        categoryDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 2 , Name = "Name"} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 2 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Category>(It.IsAny<AddCategoryCommand>()))
            .Returns(new Core.Entities.Category() { Id = 1, Name = "Name"});
        var handler = new UpdateCategoryCommandHandler(categoryDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<Inventofree.Shared.Core.Exceptions.DuplicateNameException>(() => handler.Handle(new UpdateCategoryCommand() { Id = 1, UpdatedBy = 1 , Name = "Name"},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowCategoryArgumentNullException()
    {
        //Arrange
        var categoryDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        categoryDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1 , Name = "Name"} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Category>(It.IsAny<AddCategoryCommand>()))
            .Returns(new Core.Entities.Category() { Id = 1 });
        var handler = new UpdateCategoryCommandHandler(categoryDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new UpdateCategoryCommand() { Id = 4, UpdatedBy = 1 , Name = "Names", Description = "Description"},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowUserArgumentNullException()
    {
        //Arrange
        var categoryDbContextMock = new Mock<IItemDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        categoryDbContextMock
            .Setup(a => a.Categories)
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1 , Name = "Name"} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 2 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Category>(It.IsAny<AddCategoryCommand>()))
            .Returns(new Core.Entities.Category() { Id = 1 });
        var handler = new UpdateCategoryCommandHandler(categoryDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new UpdateCategoryCommand() { Id = 1, UpdatedBy = 4 , Name = "Names", Description = "Description"},
            new CancellationToken(false)));
    }
}