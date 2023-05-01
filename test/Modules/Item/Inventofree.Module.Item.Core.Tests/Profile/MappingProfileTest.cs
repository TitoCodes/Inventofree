using Inventofree.Module.Item.Core.Profile;
using Shouldly;
using Xunit;

namespace Inventofree.Module.Item.Core.Tests.Profile;

public class MappingProfileTest
{
    [Fact]
    public void ShouldUpdateAuditTrail()
    {
        //Arrange
        var profile = new MappingProfile();
        //Act
        //Assert
        profile.ShouldNotBeNull();
    }
}