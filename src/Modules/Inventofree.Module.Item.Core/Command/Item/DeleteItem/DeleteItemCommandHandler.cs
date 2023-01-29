using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Item.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Unit>
    {
        private readonly IItemDbContext _itemDbContext;

        public DeleteItemCommandHandler(IItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }

        public async Task<Unit> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
        {
            var existingItem =
                await _itemDbContext.Items.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (existingItem == null)
                throw new Exception(string.Format(ItemErrorMessages.NotFound, nameof(Entities.Item)));


            _itemDbContext.Items.Remove(existingItem);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}