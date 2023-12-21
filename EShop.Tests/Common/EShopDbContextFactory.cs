using EShop.Application.Services;
using EShop.Domain;
using EShop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EShop.Tests.Common
{
    public class EShopDbContextFactory
    {
        public static Guid Product1Id { get; } = Guid.NewGuid();
        public static Guid Product2Id { get; } = Guid.NewGuid();
        public static Guid Product3Id { get; } = Guid.NewGuid();

        public static Guid UserAId { get; } = Guid.NewGuid();
        public static Guid UserBId { get; } = Guid.NewGuid();

        public static Guid UserASaleId { get; } = Guid.NewGuid();
        public static Guid UserBSaleId { get; } = Guid.NewGuid();

        public static Guid UserAPurchase1Id { get; } = Guid.NewGuid();
        public static Guid UserAPurchase2Id { get; } = Guid.NewGuid();
        public static Guid UserBPurchase1Id { get; } = Guid.NewGuid();

        public static EShopDbContext Create()
        {
            var options = new DbContextOptionsBuilder<EShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new EShopDbContext(options);
            context.Database.EnsureCreated();

            #region AddProducts
            context.Products.AddRange(
                new Product
                {
                    Id = Product1Id,
                    Name = "Утюг",
                    Price = 2000,
                },
                new Product
                {
                    Id = Product2Id,
                    Name = "Стиральная машина",
                    Price = 24000,
                },
                new Product
                {
                    Id = Product3Id,
                    Name = "Холодильник",
                    Price = 50000,
                });
            #endregion

            #region AddSales
            context.Sales.AddRange(
                new Sale
                {
                    Id = UserASaleId,
                    Value = 5.2,
                },
                new Sale
                {
                    Id = UserBSaleId,
                    Value = 0,
                });
            #endregion

            #region AddUsers
            context.Users.AddRange(
                new User
                {
                    Id = UserAId,
                    Name = "Иван",
                    Email = "email1@gmail.com",
                    Password = HashService.HashPassword("123"),
                    SaleId = UserASaleId,
                },
                new User
                {
                    Id = UserBId,
                    Name = "Петр",
                    Email = "email2@gmail.com",
                    Password = HashService.HashPassword("12345"),
                    SaleId = UserBSaleId,
                });
            #endregion

            #region AddPurchases
            context.Purchases.AddRange(
                new Purchase
                {
                    Id = UserAPurchase1Id,
                    Date = DateTime.Now,
                    Address = "адрес 1",
                    UserId = UserAId,
                    ProductId = Product1Id,
                    UsedPrice = 2000,
                },
                new Purchase
                {
                    Id = UserAPurchase2Id,
                    Date = DateTime.Now,
                    Address = "адрес 2",
                    UserId = UserAId,
                    ProductId = Product3Id,
                    UsedPrice = 50000,
                },
                new Purchase
                {
                    Id = UserBPurchase1Id,
                    Date = DateTime.Now,
                    Address = "адрес 3",
                    UserId = UserBId,
                    ProductId = Product2Id,
                    UsedPrice = 24000,
                });
            #endregion

            context.SaveChanges();
            return context;
        }

        public static void Destroy(EShopDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
