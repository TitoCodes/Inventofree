using Inventofree.Module.AuditTrail.Controller.v1;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAllAuditTrail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Tests.Controller;

public class AuditTrailControllerTest
{
    //TODO: Complete unit test for AuditTrailController
    [Fact]
    public async Task ShouldReturnOksResultAuditTrailList()
    {
        var mediatrMock = new Mock<IMediator>();
        var expectedItems = new List<AuditTrailDto>()
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
            .ReturnsAsync(expectedItems)
            .Verifiable();

        var sut = new AuditTrailController(mediatrMock.Object);

        var result = await sut.GetAllAuditTrailListAsync(It.IsAny<CancellationToken>());
        var okResult = result as OkObjectResult;

        mediatrMock.Verify(a => a.Send(It.IsAny<GetAllAuditTrailQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        okResult.ShouldNotBeNull();
        okResult.Value.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);
        var items = okResult.Value as IEnumerable<AuditTrailDto>;
        items.ShouldBeEquivalentTo(expectedItems);
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
}