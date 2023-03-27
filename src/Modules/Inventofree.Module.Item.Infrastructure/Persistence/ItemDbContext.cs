using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Infrastructure.Persistence
{
    public class ItemDbContext : ModuleDbContext, IItemDbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
        }

        protected override string Schema => "Item";

        public DbSet<Core.Entities.Item> Items { get; set; }
        public DbSet<Core.Entities.Price> Prices { get; set; }
        public DbSet<Core.Entities.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Core.Entities.Item>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Items)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}