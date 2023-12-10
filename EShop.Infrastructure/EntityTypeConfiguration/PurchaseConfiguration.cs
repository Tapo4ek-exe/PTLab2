using EShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.EntityTypeConfiguration
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(purchase => purchase.Id);
            builder.HasIndex(purchase => purchase.Id).IsUnique();
            builder.Property(purchase => purchase.Address).IsRequired().HasMaxLength(256);

            builder.HasOne(purchase => purchase.User)
                .WithMany(user => user.Purchases)
                .HasForeignKey(purchase => purchase.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(purchase => purchase.Product)
                .WithMany(product => product.Purchases)
                .HasForeignKey(purchase => purchase.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
