using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAuditTrailById;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Queries.AuditTrail.GetAuditTrailById;

public class GetAuditTrailByIdQueryTest
{
    [Fact]
    public void ShouldReturnAuditTrail()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var expectedAuditTrailDto =  new AuditTrailDto() { Id = 1, Details = "Details", Action = "Action" };
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new () { Id = 1, UpdatedBy = 1, Details = "Details", Action = "Action", CreatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        mapperMock.Setup(a => a.Map<AuditTrailDto>(It.IsAny<Core.Entities.AuditTrail>()))
            .Returns(new AuditTrailDto() { Id = 1, Details = "Details", Action = "Action" });
        var handler = new GetAuditTrailByIdHandler(auditTrailDbContextMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new GetAuditTrailByIdQuery() { Id = 1  },
            new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.Result.ShouldBeEquivalentTo(expectedAuditTrailDto);
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new () { Id = 2 } });
        var handler = new GetAuditTrailByIdHandler(auditTrailDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetAuditTrailByIdQuery() { Id = 1 },
            new CancellationToken(false)));
    }
}