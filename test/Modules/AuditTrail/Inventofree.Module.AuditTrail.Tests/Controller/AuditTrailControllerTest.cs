using Inventofree.Module.AuditTrail.Controller.v1;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.DeleteAuditTrail;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAllAuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAuditTrailById;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetDirectoryByCreatedDateRange;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Tests.Controller;

public class AuditTrailControllerTest
{
    [Fact]
    public async Task ShouldReturnOksResultAuditTrailList()
    {
        var mediatrMock = new Mock<IMediator>();
        var expectedAuditTrailList = new List<AuditTrailDto>()
        {
            new()
            {
                Details = "Sample Details",
                Action = "John Doe Updated an Item",
                CreatedBy = 1,
                Id = 1
            }
        };

        mediatrMock
            .Setup(a => a.Send(It.IsAny<GetAllAuditTrailQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAuditTrailList)
            .Verifiable();

        var sut = new AuditTrailController(mediatrMock.Object);

        var result = await sut.GetAllAuditTrailListAsync(It.IsAny<CancellationToken>());
        var okResult = result as OkObjectResult;

        mediatrMock.Verify(a => a.Send(It.IsAny<GetAllAuditTrailQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        okResult.ShouldNotBeNull();
        okResult.Value.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
        var auditTrailList = okResult.Value as IEnumerable<AuditTrailDto>;
        auditTrailList.ShouldBeEquivalentTo(expectedAuditTrailList);
    }

    [Fact]
    public async Task ShouldReturnOksResultAddAuditTrail()
    {
        var mediatrMock = new Mock<IMediator>();
        var expectedAuditTrail = new AuditTrailDto()
        {
            Details = "Sample Details",
            Action = "John Doe Updated an Item",
            CreatedBy = 1,
            Id = 1
        };

        mediatrMock
            .Setup(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAuditTrail.Id)
            .Verifiable();

        var sut = new AuditTrailController(mediatrMock.Object);

        var result = await sut.AddAuditTrailAsync(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>());
        var okResult = result as OkObjectResult;

        mediatrMock.Verify(a => a.Send(It.IsAny<AddAuditTrailCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        okResult.ShouldNotBeNull();
        okResult.Value.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
        okResult.Value.ShouldBeEquivalentTo(expectedAuditTrail.Id);
    }
    
    [Fact]
    public async Task ShouldReturnOksResultAuditTrailById()
    {
        var mediatrMock = new Mock<IMediator>();
        var expectedAuditTrail = new AuditTrailDto()
        {
            Details = "Sample Details",
            Action = "John Doe Updated an Item",
            CreatedBy = 1,
            Id = 1
        };

        mediatrMock
            .Setup(a => a.Send(It.IsAny<GetAuditTrailByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAuditTrail)
            .Verifiable();

        var sut = new AuditTrailController(mediatrMock.Object);

        var result = await sut.GetAuditTrailByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());
        var okResult = result as OkObjectResult;

        mediatrMock.Verify(a => a.Send(It.IsAny<GetAuditTrailByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        okResult.ShouldNotBeNull();
        okResult.Value.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
        var auditTrail = okResult.Value as AuditTrailDto;
        auditTrail.ShouldBeEquivalentTo(expectedAuditTrail);
    }

    [Fact]
    public async Task ShouldReturnNoContentResultUpdatedAuditTrail()
    {
        //Arrange
        var mediatrMock = new Mock<IMediator>();
        mediatrMock
            .Setup(a => a.Send(It.IsAny<UpdateAuditTrailCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value)
            .Verifiable();

        //Act
        var sut = new AuditTrailController(mediatrMock.Object);

        //Assert
        var result =
            await sut.UpdateAuditTrailAsync(It.IsAny<UpdateAuditTrailCommand>(), It.IsAny<CancellationToken>());
        var noContentResult = result as NoContentResult;

        mediatrMock.Verify(a => a.Send(It.IsAny<UpdateAuditTrailCommand>(), It.IsAny<CancellationToken>()),
            Times.Once);
        noContentResult.ShouldNotBeNull();
        noContentResult.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
    }
    
    [Fact]
    public async Task ShouldReturnNoContentResultDeleteAuditTrail()
    {
        //Arrange
        var mediatrMock = new Mock<IMediator>();
        mediatrMock
            .Setup(a => a.Send(It.IsAny<DeleteAuditTrailCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value)
            .Verifiable();

        //Act
        var sut = new AuditTrailController(mediatrMock.Object);

        //Assert
        var result =
            await sut.DeleteAuditTrailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>());
        var noContentResult = result as NoContentResult;

        mediatrMock.Verify(a => a.Send(It.IsAny<DeleteAuditTrailCommand>(), It.IsAny<CancellationToken>()),
            Times.Once);
        noContentResult.ShouldNotBeNull();
        noContentResult.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
    }
    
    [Fact]
    public async Task ShouldReturnOksResultAuditTrailByCreatedDateRange()
    {
        var mediatrMock = new Mock<IMediator>();
        var expectedAuditTrailList = new List<AuditTrailDto>()
        {
            new()
            {
                Details = "Sample Details",
                Action = "John Doe Updated an Item",
                CreatedBy = 1,
                Id = 1
            }
        };

        mediatrMock
            .Setup(a => a.Send(It.IsAny<GetDirectoryByCreatedDateRangeQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAuditTrailList)
            .Verifiable();

        var sut = new AuditTrailController(mediatrMock.Object);

        var result = await sut.GetAuditTrailByCreatedDateRangeAsync(It.IsAny<DateTime>(),It.IsAny<DateTime>(), It.IsAny<CancellationToken>());
        var okResult = result as OkObjectResult;

        mediatrMock.Verify(a => a.Send(It.IsAny<GetDirectoryByCreatedDateRangeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        okResult.ShouldNotBeNull();
        okResult.Value.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
        var auditTrailList = okResult.Value as List<AuditTrailDto>;
        auditTrailList.ShouldBeEquivalentTo(expectedAuditTrailList);
    }
}