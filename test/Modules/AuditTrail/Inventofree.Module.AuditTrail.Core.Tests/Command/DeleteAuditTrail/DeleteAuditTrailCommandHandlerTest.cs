using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.DeleteAuditTrail;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Command.DeleteAuditTrail;

public class DeleteAuditTrailCommandHandlerTest
{
    [Fact]
    public void ShouldDeleteAuditTrail()
    {
        //Arrange
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new() { Id = 1 } });
        var handler = new DeleteAuditTrailCommandHandler(auditTrailDbContextMock.Object);
        //Act
        var result = handler.Handle(new DeleteAuditTrailCommand() { Id = 1  },
            new CancellationToken(false));
        //Assert
        auditTrailDbContextMock.Verify(a => a.AuditTrails.Remove(It.IsAny<Core.Entities.AuditTrail>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new (){ Id = 2 } });
        var handler = new DeleteAuditTrailCommandHandler(auditTrailDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new DeleteAuditTrailCommand() { Id = 1 },
            new CancellationToken(false)));
    }
}