using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Inventofree.Module.AuditTrail.Abstractions;
using Inventofree.Module.AuditTrail.Core.Command.AuditTrail.AddAuditTrail;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Command.Price.DeletePrice;
using Inventofree.Module.Item.Core.Resources;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Item.UpdateItem
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IUserDbContext _userDbContext;
        private readonly IAuditTrailDbContext _auditTrailDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(
            IItemDbContext itemDbContext, 
            IUserDbContext userDbContext,
            IAuditTrailDbContext auditTrailDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _itemDbContext = itemDbContext;
            _userDbContext = userDbContext;
            _auditTrailDbContext = auditTrailDbContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        //#TODO: Handle delete price command and implement transaction
        public async Task<bool> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
        {
            if (await _itemDbContext.Items.AnyAsync(c => c.Name == command.Name, cancellationToken))
                throw new InvalidOperationException(string.Format(ItemErrorMessages.DuplicateName, nameof(Entities.Item)));
            
            var user = await _userDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == command.UpdatedBy, cancellationToken);

            if (user == null)
                throw new InvalidOperationException(UserErrorMessages.UserNotFound);

            var existingItem =
                await _itemDbContext.Items
                    .Include(a => a.Price)
                    .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            
            if (existingItem == null)
                throw new InvalidOperationException(ItemErrorMessages.NotFound);
            
            var serializedOldValues = JsonSerializer.Serialize(existingItem);
            existingItem.Name = command.Name;
            existingItem.Detail = command.Detail;
            existingItem.ModifiedDate = DateTimeOffset.UtcNow;
            existingItem.UpdatedBy = command.UpdatedBy;
            if (existingItem.Price.Amount.CompareTo(command.Price.Amount) != 0)
            {
                var oldPriceId = existingItem.Price.Id;
                var newPrice = _mapper.Map<Entities.Price>(command.Price);
                newPrice.ModifiedDate = DateTimeOffset.UtcNow;
                existingItem.Price = newPrice;
                var deletePriceCommand = new DeletePriceCommand()
                {
                    Id = oldPriceId
                };
                await _mediator.Send(deletePriceCommand, cancellationToken);
            }
            
            var serializedNewValues = JsonSerializer.Serialize(existingItem);
            var addAuditTrailRecord = new AddAuditTrailCommand()
            {
                Action = string.Format(AuditTrailSystemMessages.FormatAction, user.Id, "updated", existingItem.Id),
                Details = string.Format(AuditTrailSystemMessages.FormatDetails, serializedOldValues, serializedNewValues),
                CreatedBy = user.Id
            };
            
            _itemDbContext.Items.Update(existingItem);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            await _mediator.Send(addAuditTrailRecord, cancellationToken);
            
            return true;
        }
    }
}