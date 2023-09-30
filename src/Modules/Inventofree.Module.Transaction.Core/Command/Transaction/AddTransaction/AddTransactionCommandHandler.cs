using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using Inventofree.Module.Transaction.Core.Abstractions;
using Inventofree.Module.Transaction.Core.Enums;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Transaction.Core.Command.Transaction.AddTransaction;

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, Unit>
{
    private readonly IItemDbContext _itemDbContext;
    private readonly IUserDbContext _userDbContext;
    private readonly ITransactionDbContext _transactionDbContext;
    private readonly IMapper _mapper;

    public AddTransactionCommandHandler(
        IItemDbContext itemDbContext,
        IUserDbContext userDbContext,
        ITransactionDbContext transactionDbContext,
        IMapper mapper)
    {
        _itemDbContext = itemDbContext;
        _userDbContext = userDbContext;
        _transactionDbContext = transactionDbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddTransactionCommand command, CancellationToken cancellationToken)
    {
        var user = await _userDbContext.Users.FirstOrDefaultAsync(a => a.Id == command.CreatedBy,
            cancellationToken);
        if (user == null)
            throw new ArgumentNullException(UserErrorMessages.UserNotFound);
        
        var item = _itemDbContext.Items.FirstOrDefaultAsync(a => a.Id == command.ItemId, cancellationToken);
        if (item == null)
            throw new ArgumentException(string.Format(ItemErrorMessages.NotFound, command.ItemId));
        
        var transaction = _mapper.Map<Entities.Transaction>(command);
        transaction.Type = TransactionType.Purchase;
        await _transactionDbContext.Transactions.AddAsync(transaction, cancellationToken);
        await _transactionDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}