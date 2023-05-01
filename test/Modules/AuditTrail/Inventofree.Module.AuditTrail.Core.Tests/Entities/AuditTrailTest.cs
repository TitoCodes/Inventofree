using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Entities;

public class AuditTrailTest
{
    [Fact]
    public void ShouldMatchEntity()
    {
        //Arrange
        var dateCreated = DateTimeOffset.UtcNow;
        var entity = new Core.Entities.AuditTrail
        {
            Id = 1,
            CreatedDate = dateCreated,
            ModifiedDate = dateCreated,
            Action = "Action",
            Details = "Details",
            CreatedBy = 1,
            UpdatedBy = 1
        };
        //Act
        //Assert
        entity.Id.ShouldBeEquivalentTo((long)1);
        entity.UpdatedBy.ShouldBeEquivalentTo((long)1);
        entity.CreatedBy.ShouldBeEquivalentTo((long)1);
        entity.CreatedDate.ShouldBeEquivalentTo(dateCreated);
        entity.ModifiedDate.ShouldBeEquivalentTo(dateCreated);
        entity.Action.ShouldBeEquivalentTo("Action");
        entity.Details.ShouldBeEquivalentTo("Details");
    }
}