using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.User.Core.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Command.Category.AddCategory;

public class AddCategoryTest
{
    [Fact]
    public void ShouldAddCategory()
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
        mapperMock.Setup(a => a.Map<Core.Entities.Category>(It.IsAny<AddCategoryCommand>()))
            .Returns(new Core.Entities.Category() { Id = 1, Name = "Name", Description = "Description", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        var handler = new AddCategoryCommandHandler(categoryDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new AddCategoryCommand() { CreatedBy = 1, Name = "Names", Description = "Description" },
            new CancellationToken(false));
        //Assert
        categoryDbContextMock.Verify(a => a.Categories.AddAsync( It.IsAny<Core.Entities.Category>() , It.IsAny<CancellationToken>()), Times.Once);
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
            .ReturnsDbSet(new List<Core.Entities.Category>() { new() { Id = 1 , Name = "Name"} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 2 } });
        mapperMock.Setup(a => a.Map<Core.Entities.Category>(It.IsAny<AddCategoryCommand>()))
            .Returns(new Core.Entities.Category() { Id = 1 });
        var handler = new AddCategoryCommandHandler(categoryDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<Inventofree.Shared.Core.Exceptions.DuplicateNameException>(() => handler.Handle(new AddCategoryCommand() { CreatedBy = 1 , Name = "Name"},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
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
        var handler = new AddCategoryCommandHandler(categoryDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new AddCategoryCommand() { CreatedBy = 3 , Name = "Names", Description = "Description"},
            new CancellationToken(false)));
    }
}