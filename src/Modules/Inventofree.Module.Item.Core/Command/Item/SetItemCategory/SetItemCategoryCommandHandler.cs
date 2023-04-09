using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Item.SetItemCategory
{
    public class SetItemCategoryCommandHandler : IRequestHandler<SetItemCategoryCommand, Unit>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IUserDbContext _userDbContext;

        public SetItemCategoryCommandHandler(IItemDbContext itemDbContext, IUserDbContext userDbContext)
        {
            _itemDbContext = itemDbContext;
            _userDbContext = userDbContext;
        }

        public async Task<Unit> Handle(SetItemCategoryCommand command, CancellationToken cancellationToken)
        {
            var existingItem =
                await _itemDbContext.Items.FirstOrDefaultAsync(c => c.Id == command.ItemId, cancellationToken);
            if (existingItem == null)
                throw new InvalidOperationException(string.Format(ItemErrorMessages.NotFound, nameof(Entities.Item)));


            var user = await _userDbContext.Users.FirstOrDefaultAsync(a => a.Id == command.UserId,
                cancellationToken);

            if (user == null)
                throw new InvalidOperationException(UserErrorMessages.UserNotFound);


            var existingCategory =
                await _itemDbContext.Categories.FirstOrDefaultAsync(c => c.Id == command.CategoryId, cancellationToken);
            if (existingCategory == null)
                throw new InvalidOperationException(string.Format(ItemErrorMessages.NotFound, nameof(Entities.Category)));


            existingItem.CategoryId = command.CategoryId;
            existingItem.UpdatedBy = command.UserId;
            existingItem.ModifiedDate = DateTimeOffset.UtcNow;

            _itemDbContext.Items.Update(existingItem);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}