using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.User.Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.User.Core.Command.User.InsertUser
{
    public class InsertUserCommand : IRequest<long>
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        
        public string Middlename { get; set; }
        
        public string Lastname { get; set; }
    }
    
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, long>
    {
        private readonly IUserDbContext _userDbContext;

        public InsertUserCommandHandler(IUserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        
        public async Task<long> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            if (await _userDbContext.Users.AnyAsync(c => c.Email == command.Email, cancellationToken))
            {
                throw new Exception("User with the same email already exists.");
            }
            var item = new Entities.User()
            {
                Firstname = command.Firstname,
                Middlename = command.Middlename,
                Lastname = command.Lastname,
                Email = command.Email,
                CreatedDate = DateTime.UtcNow
            };
            await _userDbContext.Users.AddAsync(item, cancellationToken);
            await _userDbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}