using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Shouldly;
using Xunit;

namespace Inventofree.Module.AuditTrail.Core.Tests.Command.AddAuditTrail;

public class AddAuditTrailCommandValidatorTest
{
    [Fact]
    public void ShouldUpdateAuditTrail()
    {
        //Arrange
        var validator = new AddAuditTrailCommandValidator();
        //Act
        var result = validator.Validate(new AddAuditTrailCommand(){ Action = "Action", Details = "Details", CreatedBy = 1 });
        //Assert
        result.ShouldNotBeNull();
    }
}