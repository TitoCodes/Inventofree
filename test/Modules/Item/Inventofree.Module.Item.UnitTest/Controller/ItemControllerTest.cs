using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Controller;
using Inventofree.Module.Item.Core.Command.Item;
using Inventofree.Module.Item.Core.Queries;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.UnitTest.Controller
{
    public class ItemControllerTest
    {
        [Fact]
        public async Task ShouldReturnOksResultItemList()
        {
            var mediatrMock = new Mock<IMediator>();
            var expectedItems = new List<Core.Entities.Item>()
            {
                new()
                {
                    Detail = "Sample Details",
                    Name = "Headset",
                    Id = 1,
                    CreatedDate = new DateTime(1, 12, 12),
                    ModifiedDate = new DateTime(1, 12, 11)
                }
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<GetAllItemsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedItems)
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.GetAllAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<GetAllItemsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var items = okResult.Value as IEnumerable<Core.Entities.Item>;
            items.ShouldBeEquivalentTo(expectedItems);
        }

        [Fact]
        public async Task ShouldReturnOkResultAddedItemId()
        {
            var mediatrMock = new Mock<IMediator>();
            long expectedItemId = 1;

            mediatrMock
                .Setup(a => a.Send(It.IsAny<AddItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedItemId)
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.AddItemAsync(It.IsAny<AddItemCommand>(), It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<AddItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var itemId = (long) okResult.Value;
            itemId.ShouldBeEquivalentTo(expectedItemId);
        }

        [Fact]
        public async Task ShouldReturnBadRequestItemWithTheSameNameExists()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new AddItemCommand()
            {
                Name = "Sample name",
                Detail = "Sample detail"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<AddItemCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(ItemErrorMessages.DuplicateItemName))
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.AddItemAsync(command, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<AddItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(ItemErrorMessages.DuplicateItemName);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
        
        [Fact]
        public async Task ShouldReturnNoContentResultUpdatedItemId()
        {
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(a => a.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.UpdateItemAsync(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>());
            var noContentResult = result as NoContentResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            noContentResult.ShouldNotBeNull();
            noContentResult.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }
        
        [Fact]
        public async Task ShouldReturnBadRequestItemNotFound()
        {
            var mediatrMock = new Mock<IMediator>();
            var command = new UpdateItemCommand()
            {
                Name = "Sample name",
                Detail = "Sample detail",
                Id = 0
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(ItemErrorMessages.NotFound))
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.UpdateItemAsync(command, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<UpdateItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(ItemErrorMessages.NotFound);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
    }
}