using EShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Interfaces
{
    public interface IEShopDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
