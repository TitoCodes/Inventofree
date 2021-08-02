using System;
using System.Threading;
using System.Threading.Tasks;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Item
{
    public class RegisterItemCommand : IRequest<long>
    {
        public string Name { get; set; }

        public string Detail { get; set; }
    }

    internal class RegisterCommandHandler : IRequestHandler<RegisterItemCommand, long>
    {
        private readonly IItemDbContext _itemDbContext;

        public RegisterCommandHandler(IItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }

        public async Task<long> Handle(RegisterItemCommand command, CancellationToken cancellationToken)
        {
            if (await _itemDbContext.Items.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new Exception(ItemErrorMessages.DuplicateItemName);
            }
            var item = new Entities.Item() { Detail = command.Detail, Name = command.Name };
            await _itemDbContext.Items.AddAsync(item, cancellationToken);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}