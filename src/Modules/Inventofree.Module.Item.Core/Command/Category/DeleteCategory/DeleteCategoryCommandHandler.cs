using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Category.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IItemDbContext _itemDbContext;

        public DeleteCategoryCommandHandler(IItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var existingCategory =
                await _itemDbContext.Categories.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (existingCategory == null)
                throw new ArgumentNullException(string.Format(ItemErrorMessages.NotFound, nameof(Entities.Category)));
            
            _itemDbContext.Categories.Remove(existingCategory);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}