using AutoMapper;
using EShop.Application.Features.Products.Models;
using EShop.Application.Features.Products.Queries.GetProductList;
using EShop.Infrastructure;
using EShop.Tests.Common;
using Shouldly;

namespace EShop.Tests.Products.Queries
{
    [Collection("QueryCollection")]
    public class GetProductListQueryHandlerTests
    {
        private readonly EShopDbContext Context;
        private readonly IMapper Mapper;

        public GetProductListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetProductListHandler_WithoutPerosnalPrices_Success()
        {
            // Arrange
            var handler = new GetProductListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetProductListQuery(),
                CancellationToken.None);
            var standartPricesSum = result.Products.Select(product =>
                product.Price).Sum();
            var salePricesSum = result.Products.Select(product =>
                product.SalePrice).Sum();

            // Assert
            result.ShouldBeOfType<ProductListVm>();
            standartPricesSum.ShouldBe(salePricesSum);
            result.Products.Count().ShouldBe(3);
        }

        [Fact]
        public async Task GetProductListHandler_WithPerosnalPrices_Success()
        {
            // Arrange
            var handler = new GetProductListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetProductListQuery
                {
                    UserId = EShopDbContextFactory.UserAId,
                },
                CancellationToken.None);
            var standartPricesSum = result.Products.Select(product =>
                product.Price).Sum();
            var salePricesSum = result.Products.Select(product =>
                product.SalePrice).Sum();

            // Assert
            result.ShouldBeOfType<ProductListVm>();
            result.Products.Count().ShouldBe(3);
            standartPricesSum.ShouldBeGreaterThan(salePricesSum - 1);
            result.Products.Sum(product => product.SalePrice).ShouldBe(72048);
        }

        [Fact]
        public async Task GetProductListHandler_WithWrongUserId_Success()
        {
            // Arrange
            var handler = new GetProductListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetProductListQuery
                {
                    UserId = Guid.NewGuid(),
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<ProductListVm>();
            result.Products.Count().ShouldBe(3);
            result.Products.Sum(product => product.SalePrice).ShouldBe(76000);
        }
    }
}
