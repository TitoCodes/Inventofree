using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.UpdateAuditTrail;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Command.UpdateAuditTrail;

public class UpdateAuditTrailCommandValidatorTest
{
    [Fact]
    public void ShouldUpdateAuditTrail()
    {
        //Arrange
        var validator = new UpdateAuditTrailCommandValidator();
        //Act
        var result = validator.Validate(new UpdateAuditTrailCommand()
            {
                Id = 1,
                UpdatedBy = 1,
                Action = "Action",
                Details = "Details"
            });
        //Assert
        result.ShouldNotBeNull();
    }
}