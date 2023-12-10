using EShop.Application.Interfaces;
using EShop.Domain;
using EShop.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure
{
    public class EShopDbContext : DbContext, IEShopDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }

        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new PurchaseConfiguration());
            builder.ApplyConfiguration(new SaleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
