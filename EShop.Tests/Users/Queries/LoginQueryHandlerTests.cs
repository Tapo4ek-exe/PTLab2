using AutoMapper;
using EShop.Application.Common.Exceptions;
using EShop.Application.Features.Users.Queries.Login;
using EShop.Application.Services;
using EShop.Infrastructure;
using EShop.Tests.Common;
using Shouldly;

namespace EShop.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class LoginQueryHandlerTests
    {
        private readonly EShopDbContext Context;
        private readonly IMapper Mapper;

        public LoginQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task LoginQueryHandlerTests_Success()
        {
            // Arrange
            var handler = new LoginQueryHandler(Context);
            var jwtService = new JwtService();

            // Act
            var jwt = await handler.Handle(
                new LoginQuery
                {
                    Email = "email1@gmail.com",
                    Password = "123",
                },
                CancellationToken.None);

            // Assert
            jwt.ShouldBe(jwtService.GetJwtTokenForUser(EShopDbContextFactory.UserAId));
        }

        [Fact]
        public async Task LoginQueryHandlerTests_FailOnWrongEmail()
        {
            // Arrange
            var handler = new LoginQueryHandler(Context);

            // Assert
            // Act
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                new LoginQuery
                {
                    Email = "",
                    Password = "123",
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task LoginQueryHandlerTests_FailOnWrongPassword()
        {
            // Arrange
            var handler = new LoginQueryHandler(Context);

            // Assert
            // Act
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                new LoginQuery
                {
                    Email = "email1@gmail.com",
                    Password = "",
                },
                CancellationToken.None));
        }
    }
}
