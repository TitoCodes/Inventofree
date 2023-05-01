using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.DeleteAuditTrail;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Command.DeleteAuditTrail;

public class DeleteAuditTrailCommandValidatorTest
{
    [Fact]
    public void ShouldUpdateAuditTrail()
    {
        //Arrange
        var validator = new DeleteAuditTrailCommandValidator();
        //Act
        var result = validator.Validate(new DeleteAuditTrailCommand()
            {
                Id = 1
            });
        //Assert
        result.ShouldNotBeNull();
    }
}