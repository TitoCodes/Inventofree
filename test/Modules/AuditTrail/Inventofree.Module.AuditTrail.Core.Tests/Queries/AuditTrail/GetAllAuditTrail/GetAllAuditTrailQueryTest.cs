using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Dto.AuditTrail;
using Inventofree.Module.AuditTrail.Core.Queries.AuditTrail.GetAllAuditTrail;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Queries.AuditTrail.GetAllAuditTrail;

public class GetAllAuditTrailQueryTest
{
    [Fact]
    public void ShouldReturnAuditTrailEmptySearchString()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var expectedAuditTrailDto = new List<AuditTrailDto>() { new AuditTrailDto() { Id = 1 } };
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Entities.AuditTrail>() { new Entities.AuditTrail() { Id = 1 } });
        
        mapperMock.Setup(a => a.Map<AuditTrailDto>(It.IsAny<Entities.AuditTrail>()))
            .Returns(new AuditTrailDto() { Id = 1 });
        
        var handler = new GetAllAuditTrailHandler(auditTrailDbContextMock.Object, mapperMock.Object);
        //Act

        var result = handler.Handle(new GetAllAuditTrailQuery() { SearchString = string.Empty },
            new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.Result.ShouldBeEquivalentTo(expectedAuditTrailDto);
    }
    
    [Fact]
    public void ShouldReturnAuditTrailWithSearchString()
    {
        //Arrange
        var mapperMock = new Mock<IMapper>();
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var expectedAuditTrailDto = new List<AuditTrailDto>() { new AuditTrailDto() { Id = 1, Action = "Sample action"} };
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Entities.AuditTrail>() { new Entities.AuditTrail() { Id = 1, Action = "Sample action"} });
        
        mapperMock.Setup(a => a.Map<AuditTrailDto>(It.IsAny<Entities.AuditTrail>()))
            .Returns(new AuditTrailDto() { Id = 1, Action = "Sample action"});
        
        var handler = new GetAllAuditTrailHandler(auditTrailDbContextMock.Object, mapperMock.Object);
        //Act

        var result = handler.Handle(new GetAllAuditTrailQuery() { SearchString = "Sample action" },
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
            .ReturnsDbSet(new List<Entities.AuditTrail>());
        
        mapperMock.Setup(a => a.Map<AuditTrailDto>(It.IsAny<Entities.AuditTrail>()))
            .Returns(new AuditTrailDto() { Id = 1 });
        
        var handler = new GetAllAuditTrailHandler(auditTrailDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new GetAllAuditTrailQuery() { SearchString = string.Empty },
            new CancellationToken(false)));
    }
}