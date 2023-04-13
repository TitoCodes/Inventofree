using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Item.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Unit>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IMediator _mediator;

        public DeleteItemCommandHandler(IItemDbContext itemDbContext, IMediator mediator)
        {
            _itemDbContext = itemDbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var existingItem =
                await _itemDbContext
                    .Items
                    .Include(a => a.Price)
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (existingItem == null)
                throw new InvalidOperationException(string.Format(ItemErrorMessages.NotFound, nameof(Entities.Item)));

            _itemDbContext.Items.Remove(existingItem);
            if (existingItem.Price != null)
                _itemDbContext.Prices.Remove(existingItem.Price);
            
            var addAuditTrailRecord = new AddAuditTrailCommand()
            {
                Action = string.Format(AuditTrailSystemMessages.FormatAction, "deleted", 1, existingItem.Id),
                Details = string.Format(AuditTrailSystemMessages.FormatDetails, JsonSerializer.Serialize(existingItem), "N/A"),
                CreatedBy = 1
            };
            
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            await _mediator.Send(addAuditTrailRecord, cancellationToken);
            return Unit.Value;
        }
    }
}