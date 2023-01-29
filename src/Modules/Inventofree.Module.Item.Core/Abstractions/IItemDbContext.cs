using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Abstractions
{
    public interface IItemDbContext
    {
        public DbSet<Entities.Item> Items { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}