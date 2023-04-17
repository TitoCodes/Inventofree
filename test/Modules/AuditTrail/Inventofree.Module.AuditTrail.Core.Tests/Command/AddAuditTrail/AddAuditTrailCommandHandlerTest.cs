using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.User.Core.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Command.AddAuditTrail;

public class AddAuditTrailCommandHandlerTest
{
    [Fact]
    public void ShouldAddAuditTrail()
    {
        //Arrange
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new() { Id = 1, Action = "Action", Details = "Details", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now} });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 1 } });
        mapperMock.Setup(a => a.Map<Core.Entities.AuditTrail>(It.IsAny<AddAuditTrailCommand>()))
            .Returns(new Core.Entities.AuditTrail() { Id = 1, Details = "Details", Action = "Action", CreatedBy = 1, UpdatedBy = 1, CreatedDate = DateTimeOffset.Now, ModifiedDate = DateTimeOffset.Now });
        var handler = new AddAuditTrailCommandHandler(auditTrailDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        var result = handler.Handle(new AddAuditTrailCommand() { CreatedBy = 1, Action = "Action", Details = "Details" },
            new CancellationToken(false));
        //Assert
        auditTrailDbContextMock.Verify(a => a.AuditTrails.AddAsync(It.IsAny<Core.Entities.AuditTrail>(), It.IsAny<CancellationToken>()), Times.Once);
        auditTrailDbContextMock.Verify(a => a.SaveChangesAsync( It.IsAny<CancellationToken>()), Times.Once);
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void ShouldThrowArgumentNullException()
    {
        //Arrange
        var auditTrailDbContextMock = new Mock<IAuditTrailDbContext>();
        var userDbContextMock = new Mock<IUserDbContext>();
        var mapperMock = new Mock<IMapper>();
        auditTrailDbContextMock
            .Setup(a => a.AuditTrails)
            .ReturnsDbSet(new List<Core.Entities.AuditTrail>() { new() { Id = 1 } });
        userDbContextMock
            .Setup(a => a.Users)
            .ReturnsDbSet(new List<User.Core.Entities.User>() { new() { Id = 2 } });
        mapperMock.Setup(a => a.Map<Core.Entities.AuditTrail>(It.IsAny<AddAuditTrailCommand>()))
            .Returns(new Core.Entities.AuditTrail() { Id = 1 });
        var handler = new AddAuditTrailCommandHandler(auditTrailDbContextMock.Object, userDbContextMock.Object, mapperMock.Object);
        //Act
        //Assert
        Should.Throw<ArgumentNullException>(() => handler.Handle(new AddAuditTrailCommand() { CreatedBy = 1, Action = "Action", Details = "Details" },
            new CancellationToken(false)));
    }
}