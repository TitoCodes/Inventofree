using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Controller.v1;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Category.UpdateCategory;
using Inventofree.Module.Item.Core.Command.Item.UpdateItem;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.UnitTest.Controller
{
    public class CategoryControllerTest
    {
        [Fact]
        public async Task ShouldReturnOkResultAddedCategoryId()
        {
            var mediatrMock = new Mock<IMediator>();
            long expectedItemId = 1;

            mediatrMock
                .Setup(a => a.Send(It.IsAny<AddCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedItemId)
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.AddCategoryAsync(It.IsAny<AddCategoryCommand>(), It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<AddCategoryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var itemId = (long) okResult.Value;
            itemId.ShouldBeEquivalentTo(expectedItemId);
        }

        [Fact]
        public async Task ShouldReturnBadRequestCategoryWithTheSameNameExists()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new AddCategoryCommand()
            {
                Name = "Sample name",
                Description = "Sample detail"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<AddCategoryCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(ItemErrorMessages.DuplicateName))
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.AddCategoryAsync(command, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<AddCategoryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(ItemErrorMessages.DuplicateName);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task ShouldReturnNoContentResultUpdatedItemId()
        {
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(a => a.Send(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.UpdateCategoryAsync(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>());
            var noContentResult = result as NoContentResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            noContentResult.ShouldNotBeNull();
            noContentResult.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }
        
        [Fact]
        public async Task ShouldReturnBadRequestItemNotFound()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new UpdateCategoryCommand()
            {
                Name = "Sample name",
                Description = "Sample detail",
                Id = 0
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(ItemErrorMessages.NotFound))
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.UpdateCategoryAsync(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>());
            var noContentResult = result as NoContentResult;
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(ItemErrorMessages.NotFound);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
    }
}