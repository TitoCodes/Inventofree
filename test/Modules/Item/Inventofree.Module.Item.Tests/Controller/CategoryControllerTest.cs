using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Controller.v1;
using Inventofree.Module.Item.Core.Command.Category.AddCategory;
using Inventofree.Module.Item.Core.Command.Category.DeleteCategory;
using Inventofree.Module.Item.Core.Command.Category.UpdateCategory;
using Inventofree.Module.Item.Core.Entities;
using Inventofree.Module.Item.Core.Queries.Category.GetAllCategories;
using Inventofree.Module.Item.Core.Queries.Category.GetCategoryById;
using Inventofree.Module.Item.Core.Queries.Category.GetCategoryByName;
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
        public async Task ShouldReturnOksResultCategoryByName()
        {
            var mediatrMock = new Mock<IMediator>();
            var expectedCategory = new Category()

            {
                Description = "Sample category description",
                Name = "Headset",
                Id = 1,
                CreatedDate = new DateTime(1, 12, 12),
                ModifiedDate = new DateTime(1, 12, 11)
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<GetCategoryByNameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCategory)
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.GetCategoryByName(It.IsAny<string>(), It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<GetCategoryByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var items = okResult.Value as Category;
            items.ShouldBeEquivalentTo(expectedCategory);
        }
        
        [Fact]
        public void ShouldReturnBadRequestResultCategoryByName()
        {
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(a => a.Send(It.IsAny<GetCategoryByNameQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Category))))
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);
            
            Should.Throw<Exception>(async () => await sut.GetCategoryByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Message.ShouldBe(string.Format(ItemErrorMessages.NotFound, nameof(Category)));
        }
        
        [Fact]
        public void ShouldReturnBadRequestResultCategoryById()
        {
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(a => a.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Category))))
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            Should.Throw<Exception>(async () => await sut.GetCategoryById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Message.ShouldBe(string.Format(ItemErrorMessages.NotFound, nameof(Category)));
        }

        [Fact]
        public async Task ShouldReturnOksResultCategoryList()
        {
            var mediatrMock = new Mock<IMediator>();
            var expectedCategories = new List<Category>()
            {
                new()
                {
                    Description = "Sample Category Description",
                    Name = "Smart Phone",
                    Id = 1,
                    CreatedDate = new DateTime(1, 12, 12),
                    ModifiedDate = new DateTime(1, 12, 11)
                }
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<GetAllCategoriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCategories)
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.GetAllCategoriesAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<GetAllCategoriesQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var items = okResult.Value as IEnumerable<Category>;
            items.ShouldBeEquivalentTo(expectedCategories);
        }
        
        [Fact]
        public async Task ShouldReturnOksResultCategoryById()
        {
            var mediatrMock = new Mock<IMediator>();
            var expectedCategory = new Category()

            {
                Description = "Sample category description",
                Name = "Headset",
                Id = 1,
                CreatedDate = new DateTime(1, 12, 12),
                ModifiedDate = new DateTime(1, 12, 11)
            };

            mediatrMock
                .Setup(a => a.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCategory)
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.GetCategoryById(It.IsAny<int>(), It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            okResult.ShouldNotBeNull();
            okResult.Value.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
            var items = okResult.Value as Category;
            items.ShouldBeEquivalentTo(expectedCategory);
        }

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
            var itemId = (long)okResult.Value;
            itemId.ShouldBeEquivalentTo(expectedItemId);
        }

        [Fact]
        public void ShouldReturnBadRequestCategoryWithTheSameNameExists()
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
            
            Should.Throw<Exception>(async () => await sut.AddCategoryAsync(command, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(ItemErrorMessages.DuplicateName);
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

            var result =
                await sut.UpdateCategoryAsync(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>());
            var noContentResult = result as NoContentResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
            noContentResult.ShouldNotBeNull();
            noContentResult.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void ShouldReturnBadRequestItemNotFound()
        {
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(a => a.Send(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Category))))
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            Should.Throw<Exception>(async () => await sut.UpdateCategoryAsync(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()))
                .Message.ShouldBe(string.Format(ItemErrorMessages.NotFound, nameof(Category)));
        }

        [Fact]
        public async Task ShouldReturnNoContentResultDeleteCategoryId()
        {
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(a => a.Send(It.IsAny<DeleteCategoryCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);

            var result = await sut.DeleteCategoryAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());
            var noContentResult = result as NoContentResult;

            mediatrMock.Verify(a => a.Send(It.IsAny<DeleteCategoryCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
            noContentResult.ShouldNotBeNull();
            noContentResult.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void ShouldReturnBadRequestDeleteCategoryNotFound()
        {
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(a => a.Send(It.IsAny<DeleteCategoryCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Category))))
                .Verifiable();

            var sut = new CategoryController(mediatrMock.Object);
            
            Should.Throw<Exception>(async () => await sut.DeleteCategoryAsync(1, It.IsAny<CancellationToken>()))
                .Message.ShouldBe(string.Format(ItemErrorMessages.NotFound, nameof(Category)));
        }
    }
}