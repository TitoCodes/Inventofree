using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Item
{
    public class UpdateItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Detail { get; set; }

        public int UpdatedBy { get; set; }
    }

    internal class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IUserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(IItemDbContext itemDbContext, IUserDbContext userDbContext, IMapper mapper)
        {
            _itemDbContext = itemDbContext;
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
        {
            if (await _itemDbContext.Items.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new Exception(ItemErrorMessages.DuplicateItemName);
            }
            
            var user = await _userDbContext.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == command.UpdatedBy, cancellationToken);
            
            if (user == null)
            {
                throw new Exception(UserErrorMessages.UserNotFound);    
            }

            var existingItem = await _itemDbContext.Items.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (existingItem == null)
            {
                throw new Exception(ItemErrorMessages.NotFound);    
            }

            existingItem.Name = command.Name;
            existingItem.Detail = command.Detail;
            existingItem.ModifiedDate = DateTimeOffset.UtcNow;
            existingItem.UpdatedBy = command.UpdatedBy;
            
            _itemDbContext.Items.Update(existingItem);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}