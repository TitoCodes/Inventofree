using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Helpers;
using Inventofree.Module.User.Core.Resources;
using Inventofree.Shared.Core.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.User.Core.Command.User.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
{
    private readonly IUserDbContext _userDbContext;

    public CreateUserCommandHandler(IUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<long> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (!UtilityHelper.IsValidEmail(command.Email))
        {
            throw new InvalidOperationException(UserErrorMessages.InvalidEmailFormat);
        }

        if (await _userDbContext.Users.AnyAsync(c => c.Email == command.Email, cancellationToken))
        {
            throw new InvalidOperationException(UserErrorMessages.UserAlreadyExists);
        }

        if (!command.Password.Equals(command.ConfirmPassword))
        {
            throw new InvalidOperationException(UserErrorMessages.PasswordDoesntMatch);
        }

        var salt = EncryptionHelper.GenerateSalt();
        var passwordHash = EncryptionHelper.GeneratePasswordHash(command.Password, salt);

        var item = new Entities.User()
        {
            Firstname = command.Firstname,
            Middlename = command.Middlename,
            Lastname = command.Lastname,
            Email = command.Email,
            CreatedDate = DateTime.UtcNow,
            Salt = salt,
            PasswordHash = passwordHash
        };
        await _userDbContext.Users.AddAsync(item, cancellationToken);
        await _userDbContext.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}