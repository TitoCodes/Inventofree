using Inventofree.Module.Transaction.Core.Abstractions;
using Inventofree.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Transaction.Infrastructure.Persistence;

public class TransactionDbContext: ModuleDbContext, ITransactionDbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {
    }

    protected override string Schema => "Transaction";
        
    public DbSet<Core.Entities.Transaction> Transactions { get; set; }
}