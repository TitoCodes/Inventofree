using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Controller.v1;
using Inventofree.Module.Item.Core.Command.Item.AddItem;
using Inventofree.Module.Item.Core.Command.Item.DeleteItem;
using Inventofree.Module.Item.Core.Command.Item.SetItemCategory;
using Inventofree.Module.Item.Core.Command.Item.UpdateItem;
using Inventofree.Module.Item.Core.Dto.Item;
using Inventofree.Module.Item.Core.Queries.Item.GetAllItems;
using Inventofree.Module.Item.Core.Queries.Item.GetItemById;
using Inventofree.Module.Item.Core.Queries.Item.GetItemsByName;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var expectedItems = new List<ItemDto>()
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

            var result = await sut.GetAllItemsAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<GetAllItemsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var items = okResult.Value as IEnumerable<ItemDto>;
            items.ShouldBeEquivalentTo(expectedItems);
        }

        [Fact]
        public async Task ShouldReturnItemListMatchingByName()
        {
            var mediatrMock = new Mock<IMediator>();
            var expectedItems = new List<ItemDto>()
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
                .Setup(a => a.Send(It.IsAny<GetItemsByNameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedItems)
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.GetItemsByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<GetItemsByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var items = okResult.Value as IEnumerable<ItemDto>;
            items.ShouldBeEquivalentTo(expectedItems);
        }

        [Fact]
        public async Task ShouldReturnItemById()
        {
            var mediatrMock = new Mock<IMediator>();
            var expectedItem = new ItemDto()
            {
                Detail = "Sample Details",
                Name = "Headset",
                Id = 1,
                CreatedDate = new DateTime(1, 12, 12),
                ModifiedDate = new DateTime(1, 12, 11)
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<GetItemByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedItem)
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.GetItemByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<GetItemByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var items = okResult.Value as ItemDto;
            items.ShouldBeEquivalentTo(expectedItem);
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
            var itemId = (long)okResult.Value;
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
                .Throws(new Exception(ItemErrorMessages.DuplicateName))
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);
            
            Should.Throw<Exception>(async () => await sut.AddItemAsync(command, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(ItemErrorMessages.DuplicateName);
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
        public async Task ShouldReturnNoContentResultUpdatedItemCategory()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(a => a.Send(It.IsAny<SetItemCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value)
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result =
                await sut.SetItemCategoryAsync(It.IsAny<SetItemCategoryCommand>(), It.IsAny<CancellationToken>());
            var noContentResult = result as NoContentResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<SetItemCategoryCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
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
                .Throws(new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Core.Entities.Item))))
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);
            
            Should.Throw<Exception>(async () => await sut.UpdateItemAsync(command, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(string.Format(ItemErrorMessages.NotFound, nameof(Core.Entities.Item)));
        }

        [Fact]
        public async Task ShouldReturnNoContentResultDeleteItemId()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(a => a.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);

            var result = await sut.DeleteItemAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());
            var noContentResult = result as NoContentResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            noContentResult.ShouldNotBeNull();
            noContentResult.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task ShouldReturnBadRequestDeleteItemNotFound()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(a => a.Send(It.IsAny<DeleteItemCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Core.Entities.Item))))
                .Verifiable();

            var sut = new ItemController(mediatrMock.Object);
            
            Should.Throw<Exception>(async () => await sut.DeleteItemAsync(1, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(string.Format(ItemErrorMessages.NotFound, nameof(Core.Entities.Item)));
        }
    }
}