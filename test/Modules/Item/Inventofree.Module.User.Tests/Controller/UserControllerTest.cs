using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.User.Core.Command.User.InsertUser;
using Inventofree.Module.User.Core.Resources;
using Inventofree.Module.User.Controller;
using Inventofree.Module.User.Controller.v1;
using Inventofree.Module.User.Core.Command.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Inventofree.Module.User.UnitTest.Controller
{
    public class UserControllerTest
    {
        [Fact]
        public void ShouldReturnBadRequestInvalidEmailFormat()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new CreateUserCommand()
            {
                Email = "invalidEmail"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(UserErrorMessages.InvalidEmailFormat))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            Should.Throw<Exception>(async () => await sut.InsertUser(command, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(UserErrorMessages.InvalidEmailFormat);
        }
        
        [Fact]
        public void ShouldReturnBadRequestUserAlreadyExists()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new CreateUserCommand()
            {
                Email = "juan@delacruz.email"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(UserErrorMessages.UserAlreadyExists))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            Should.Throw<Exception>(async () => await sut.InsertUser(command, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(UserErrorMessages.UserAlreadyExists);
        }
        
        [Fact]
        public void ShouldReturnBadRequestPasswordMismatch()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new CreateUserCommand()
            {
                Email = "juan@delacruz.email",
                Password = "weakpass",
                ConfirmPassword = "ultraweak"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(UserErrorMessages.PasswordDoesntMatch))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            Should.Throw<Exception>(async () => await sut.InsertUser(null, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(UserErrorMessages.PasswordDoesntMatch);
        }
        
        [Fact]
        public void ShouldReturnBadRequestArgumentNullException()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(a => a.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new ArgumentException(nameof(CreateUserCommand)))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);
            
            Should.Throw<ArgumentException>(async () => await sut.InsertUser(null, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(nameof(CreateUserCommand));
        }
        
        [Fact]
        public async Task ShouldReturnOkResultAddedUserId()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new CreateUserCommand()
            {
                Email = "juan@delacruz.email"
            };
            long expectedUserId = 1;

            mediatrMock
                .Setup(a => a.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUserId)
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            var result = await sut.InsertUser(command, It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            okResult.Value.ShouldBe(expectedUserId);
        }
    }
}