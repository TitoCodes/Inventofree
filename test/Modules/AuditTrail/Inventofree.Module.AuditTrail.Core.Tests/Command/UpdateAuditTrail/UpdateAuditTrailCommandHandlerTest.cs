using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;
using Inventofree.Module.User.Core.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Command.UpdateAuditTrail;

public class UpdateAuditTrailCommandHandlerTest
{
    [Fact]
    public void ShouldUpdateAuditTrail()
    {
        //Arrange
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new() { Id = 1 } });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new UpdateAuditTrailCommandHandler(auditTrailDbContextMock.Object, userDbContextMock.Object);
        //Act
        var result = handler.Handle(new UpdateAuditTrailCommand()
            {
                Id = 1,
                UpdatedBy = 1,
                Action = "Action",
                Details = "Details"
            },
            new CancellationToken(false));
        //Assert
        auditTrailDbContextMock.Verify(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowArgumentNullUserException()
    {
        //Arrange
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new() { Id = 2 } });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 2 } });
        var handler = new UpdateAuditTrailCommandHandler(auditTrailDbContextMock.Object, userDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new UpdateAuditTrailCommand() { Id = 1, UpdatedBy = 1},
            new CancellationToken(false)));
    }
    
    [Fact]
    public void ShouldThrowArgumentNullAuditTrailException()
    {
        //Arrange
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        var handler = new UpdateAuditTrailCommandHandler(auditTrailDbContextMock.Object, userDbContextMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new UpdateAuditTrailCommand() { Id = 1, UpdatedBy = 1},
            new CancellationToken(false)));
    }
}