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

namespace Inventofree.Module.Item.Core.Command.Item.AddItem
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, long>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IUserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public AddItemCommandHandler(IItemDbContext itemDbContext, IUserDbContext userDbContext, IMapper mapper)
        {
            _itemDbContext = itemDbContext;
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(AddItemCommand command, CancellationToken cancellationToken)
        {
            if (await _itemDbContext.Items.AnyAsync(c => c.Name == command.Name, cancellationToken))
                throw new Exception(string.Format(ItemErrorMessages.DuplicateName, nameof(Entities.Item)));

            var user = await _userDbContext.Users.FirstOrDefaultAsync(a => a.Id == command.CreatedBy,
                cancellationToken);

            if (user == null)
                throw new Exception(UserErrorMessages.UserNotFound);
            
            if (command.CategoryId != null)
            {
                var category =
                    await _itemDbContext.Categories.FirstOrDefaultAsync(a => a.Id == command.CategoryId,
                        cancellationToken);
                if (category == null)
                    throw new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Entities.Category)));
            }

            var item = _mapper.Map<Entities.Item>(command);
            await _itemDbContext.Items.AddAsync(item, cancellationToken);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}