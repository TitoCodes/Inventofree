using Shouldly;
using Xunit;

namespace Inventofree.Module.User.Core.Tests.Entities;

public class UserTest
{
    [Fact]
    public void ShouldMatchUserEntity()
    {
        //Arrange
        var createdDate = DateTimeOffset.UtcNow;
        var modifiedDate = DateTimeOffset.UtcNow;
        var salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        var modifiedBy = new Core.Entities.User()
        {
            Id = 2
        };
        var user = new Inventofree.Module.User.Core.Entities.User
        {
            Id = 1,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            Email = "test@email.com",
            Firstname = "John",
            Middlename = "M",
            Lastname = "Doe",
            PasswordHash = "someHash",
            Salt = salt,
            ModifiedId = 2,
            Modified = modifiedBy
        };
        //Act
        //Assert
        user.Id.ShouldBe(1);
        user.CreatedDate.ShouldBeEquivalentTo(createdDate);
        user.ModifiedDate.ShouldBeEquivalentTo(modifiedDate);
        user.Email.ShouldBe("test@email.com");
        user.Firstname.ShouldBe("John");
        user.Middlename.ShouldBe("M");
        user.Lastname.ShouldBe("Doe");
        user.PasswordHash.ShouldBe("someHash");
        user.Salt.ShouldBeEquivalentTo(salt);
        user.ModifiedId.ShouldBe(2);
        user.Modified.ShouldBeEquivalentTo(modifiedBy);
    }
}