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

namespace Inventofree.Module.Item.Core.Command.Category.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IUserDbContext _userDbContext;

        public UpdateCategoryCommandHandler(IItemDbContext itemDbContext, IUserDbContext userDbContext, IMapper mapper)
        {
            _itemDbContext = itemDbContext;
            _userDbContext = userDbContext;
        }

        public async Task<bool> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (await _itemDbContext.Categories.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new Exception(string.Format(ItemErrorMessages.DuplicateName, nameof(Entities.Category)));
            }

            var user = await _userDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == command.UpdatedBy, cancellationToken);

            if (user == null)
            {
                throw new Exception(UserErrorMessages.UserNotFound);
            }

            var existingCategory =
                await _itemDbContext.Categories.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

            if (existingCategory == null)
            {
                throw new Exception(ItemErrorMessages.NotFound);
            }

            existingCategory.Name = command.Name;
            existingCategory.Description = command.Description;
            existingCategory.ModifiedDate = DateTimeOffset.UtcNow;
            existingCategory.UpdatedBy = command.UpdatedBy;

            _itemDbContext.Categories.Update(existingCategory);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}