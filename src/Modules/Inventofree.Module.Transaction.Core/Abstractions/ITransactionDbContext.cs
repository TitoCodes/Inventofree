using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Transaction.Core.Abstractions;

public interface ITransactionDbContext
{
    public DbSet<Entities.Transaction> Transactions { get; set; }
        
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}