using EShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.EntityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.Name).IsRequired().HasMaxLength(256);
            builder.Property(user => user.Email).HasMaxLength(256);
            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.Password).HasMaxLength(256);

            builder.HasOne(user => user.Sale)
                .WithOne(sale => sale.User)
                .HasForeignKey<User>(user => user.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
