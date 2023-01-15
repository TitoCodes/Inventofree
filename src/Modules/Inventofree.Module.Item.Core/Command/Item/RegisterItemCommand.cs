using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Item
{
    public class RegisterItemCommand : IRequest<long>
    {
        public string Name { get; set; }

        public string Detail { get; set; }

        public int CreatedBy { get; set; }
    }

    internal class RegisterCommandHandler : IRequestHandler<RegisterItemCommand, long>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IUserDbContext _userDbContext;

        public RegisterCommandHandler(IItemDbContext itemDbContext, IUserDbContext userDbContext)
        {
            _itemDbContext = itemDbContext;
            _userDbContext = userDbContext;
        }

        public async Task<long> Handle(RegisterItemCommand command, CancellationToken cancellationToken)
        {
            if (await _itemDbContext.Items.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new Exception(ItemErrorMessages.DuplicateItemName);
            }

            var user = await _userDbContext.Users.FirstOrDefaultAsync(a => a.Id == command.CreatedBy, cancellationToken);
            
            if (user == null)
            {
                throw new Exception(UserErrorMessages.UserNotFound);    
            }
            
            var item = new Entities.Item()
            {
                Detail = command.Detail,
                Name = command.Name,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = command.CreatedBy
            };
            
            await _itemDbContext.Items.AddAsync(item, cancellationToken);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}