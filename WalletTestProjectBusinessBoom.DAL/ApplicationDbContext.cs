using Microsoft.EntityFrameworkCore;
using WalletTestProjectBusinessBoom.Core.Entities;
using WalletTestProjectBusinessBoom.Core.Interfaces;

namespace WalletTestProjectBusinessBoom.DAL
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IDateFixEntity>())
            {
                entry.Entity.DateStart = entry.State switch
                {
                    EntityState.Added => DateTime.Now,
                    _ => entry.Entity.DateStart
                };
                entry.Entity.DateUpdate = entry.State switch
                {
                    EntityState.Modified => DateTime.Now,
                    _ => entry.Entity.DateUpdate
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
