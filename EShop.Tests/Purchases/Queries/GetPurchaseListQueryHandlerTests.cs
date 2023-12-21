using AutoMapper;
using EShop.Application.Features.Purchases.Models;
using EShop.Application.Features.Purchases.Queries.GetPurchaseList;
using EShop.Infrastructure;
using EShop.Tests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Tests.Purchases.Queries
{
    [Collection("QueryCollection")]
    public class GetPurchaseListQueryHandlerTests
    {
        private readonly EShopDbContext Context;
        private readonly IMapper Mapper;

        public GetPurchaseListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPurchaseListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetPurchaseListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetPurchaseListQuery 
                { 
                    UserId = EShopDbContextFactory.UserAId,
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PurchaseListVm>();
            result.Purchases.Count().ShouldBe(2);
        }
    }
}
