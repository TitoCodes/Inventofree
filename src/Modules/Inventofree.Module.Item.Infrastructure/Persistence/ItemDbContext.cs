using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Infrastructure.Persistence
{
    public class ItemDbContext : ModuleDbContext, IItemDbContext
    {
        public ItemDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override string Schema => "Item";
        
        public DbSet<Core.Entities.Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}