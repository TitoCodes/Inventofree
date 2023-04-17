using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAuditTrailByCreatedDateRange;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Queries.AuditTrail.GetAuditTrailByCreatedDateRange;

public class GetAuditTrailByCreatedDateRangeQueryTest
{
    [Fact]
    public void ShouldReturnAuditTrailWithinDateRange()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var expectedAuditTrailDto = new List<AuditTrailDto>() { new AuditTrailDto() { Id = 1 } };
        var startDate = DateTimeOffset.Now.AddDays(-1);
        var endDate = DateTimeOffset.Now.AddDays(1);
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new () { Id = 1, CreatedDate = startDate } });
        mapperMock
            .Setup(a => a.Map<IReadOnlyCollection<Core.Entities.AuditTrail>,IReadOnlyCollection<AuditTrailDto>>(It.IsAny<IReadOnlyCollection<Core.Entities.AuditTrail>>()))
            .Returns(new List<AuditTrailDto>() { new AuditTrailDto() { Id = 1 } });
        var handler = new GetAuditTrailByCreatedDateRangeQueryHandler(auditTrailDbContextMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new GetAuditTrailByCreatedDateRangeQuery() { StartDate = startDate, EndDate = endDate },
            new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.Result.ShouldBeEquivalentTo(expectedAuditTrailDto);
    }
}