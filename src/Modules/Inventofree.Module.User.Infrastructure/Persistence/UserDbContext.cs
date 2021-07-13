using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.User.Infrastructure.Persistence
{
    public class UserDbContext : ModuleDbContext, IUserDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override string Schema => "User";
        
        public DbSet<Core.Entities.User> Users { get; set; }
    }
}