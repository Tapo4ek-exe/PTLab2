using AutoMapper;
using EShop.Application.Common.Exceptions;
using EShop.Application.Features.Users.Queries.GetName;
using EShop.Infrastructure;
using EShop.Tests.Common;
using Shouldly;

namespace EShop.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class GetUserNameQueryHandlerTests
    {
        private readonly EShopDbContext Context;
        private readonly IMapper Mapper;

        public GetUserNameQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async void GetUserNameQueryHandler_Success()
        {
            // Arrange
            var handler = new GetUserNameQueryHandler(Context);

            // Act
            var name = await handler.Handle(
                new GetUserNameQuery
                {
                    UserId = EShopDbContextFactory.UserAId,
                },
                CancellationToken.None);

            // Assert
            name.ShouldBe("Иван");
        }

        [Fact]
        public async void GetUserNameQueryHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new GetUserNameQueryHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetUserNameQuery
                    {
                        UserId = Guid.NewGuid(),
                    },
                    CancellationToken.None));
        }
    }
}
