using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Command.User.CreateUser;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.User.Core.Tests.Command.CreateUser;

public class CreateUserCommandHandlerTest
{
    [Fact]
    public void ShouldCreateUser()
    {
        //Arrange
        var userDbContextMock = new Mock<IUserDbContext>();
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new CreateUserCommandHandler(userDbContextMock.Object);
        //Act
        var result = handler.Handle(new CreateUserCommand()
            {
                Firstname = "Firstname",
                Middlename = "MiddleName",
                Lastname = "Lastname",
                Email = "test@email.com",
                Password = "Password",
                ConfirmPassword = "Password"
            },
            new CancellationToken(false));
        //Assert
        userDbContextMock.Verify(a => a.Users.AddAsync(It.IsAny<Core.Entities.User>(), It.IsAny<CancellationToken>()), Times.Once);
        userDbContextMock.Verify(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var userDbContextMock = new Mock<IUserDbContext>();
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new CreateUserCommandHandler(userDbContextMock.Object);
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(null, new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationInvalidEMailException()
    {
        //Arrange
        var userDbContextMock = new Mock<IUserDbContext>();
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new CreateUserCommandHandler(userDbContextMock.Object);
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new CreateUserCommand()
            {
                Firstname = "Firstname",
                Middlename = "MiddleName",
                Lastname = "Lastname",
                Email = "test",
                Password = "Password",
                ConfirmPassword = "Password"
            },
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationUserAlreadyExistsException()
    {
        //Arrange
        var userDbContextMock = new Mock<IUserDbContext>();
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 , Email = "test@email.com"} });
        var handler = new CreateUserCommandHandler(userDbContextMock.Object);
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new CreateUserCommand()
            {
                Firstname = "Firstname",
                Middlename = "MiddleName",
                Lastname = "Lastname",
                Email = "test@email.com",
                Password = "Password",
                ConfirmPassword = "Password"
            },
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationPasswordDoesntMatchException()
    {
        //Arrange
        var userDbContextMock = new Mock<IUserDbContext>();
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 , Email = "tests@email.com"} });
        var handler = new CreateUserCommandHandler(userDbContextMock.Object);
        //Assert
        Should.Throw<InvalidOperationException>(() => handler.Handle(new CreateUserCommand()
            {
                Firstname = "Firstname",
                Middlename = "MiddleName",
                Lastname = "Lastname",
                Email = "test@email.com",
                Password = "Password",
                ConfirmPassword = "Passsword"
            },
            new CancellationToken(false)));
    }
}