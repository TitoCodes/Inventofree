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
                .Setup(a => a.Send(It.IsAny<RegisterItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedItemId)
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.RegisterItemAsync(It.IsAny<RegisterItemCommand>(), It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<RegisterItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
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
            var command = new RegisterItemCommand()
            {
                Name = "Sample name",
                Detail = "Sample detail"
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<RegisterItemCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(ItemErrorMessages.DuplicateItemName))
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.RegisterItemAsync(command, It.IsAny<CancellationToken>());
            var badReqResult = result as BadRequestObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<RegisterItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            badReqResult.ShouldNotBeNull();
            badReqResult.Value.ShouldBe(ItemErrorMessages.DuplicateItemName);
            badReqResult.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
    }
}