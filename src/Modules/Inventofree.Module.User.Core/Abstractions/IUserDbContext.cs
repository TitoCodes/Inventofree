using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.User.Core.Abstractions
{
    public interface IUserDbContext
    {
        public DbSet<Entities.User> Users { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}