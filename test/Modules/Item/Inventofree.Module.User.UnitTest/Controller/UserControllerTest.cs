using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.User.Core.Command.User.InsertUser;
using Inventofree.Module.User.Core.Resources;
using Inventofree.Module.User.Controller;
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
        public async Task ShouldReturnBadRequestInvalidEmailFormat()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new InsertUserCommand()
            {
                Email = "invalidEmail"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(UserErrorMessages.InvalidEmailFormat))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            var result = await sut.InsertUser(command, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(UserErrorMessages.InvalidEmailFormat);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task ShouldReturnBadRequestUserAlreadyExists()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new InsertUserCommand()
            {
                Email = "juan@delacruz.email"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(UserErrorMessages.UserAlreadyExists))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            var result = await sut.InsertUser(command, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(UserErrorMessages.UserAlreadyExists);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task ShouldReturnBadRequestPasswordMismatch()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new InsertUserCommand()
            {
                Email = "juan@delacruz.email",
                Password = "weakpass",
                ConfirmPassword = "ultraweak"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(UserErrorMessages.PasswordDoesntMatch))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            var result = await sut.InsertUser(command, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(UserErrorMessages.PasswordDoesntMatch);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task ShouldReturnBadRequestArgumentNullException()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new ArgumentException(nameof(InsertUserCommand)))
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            var result = await sut.InsertUser(null, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(nameof(InsertUserCommand));
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task ShouldReturnOkResultAddedUserId()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new InsertUserCommand()
            {
                Email = "juan@delacruz.email"
            };
            long expectedUserId = 1;

            mediatrMock
                .Setup(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUserId)
                .Verifiable();

            var sut = new UserController(mediatrMock.Object);

            var result = await sut.InsertUser(command, It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<InsertUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            okResult.Value.ShouldBe(expectedUserId);
        }
    }
}