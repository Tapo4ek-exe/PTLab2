using EShop.Application.Common.Exceptions;
using EShop.Application.Features.Sales.Commands.UpdateSale;
using EShop.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace EShop.Tests.Sales.Commands
{
    public class UpdateSaleCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateSaleCommandHandler_Succcess()
        {
            // Arrange
            var handler = new UpdateSaleCommandHandler(Context);
            var oldSaleValue = (await Context.Users.FirstOrDefaultAsync(user =>
                user.Id == EShopDbContextFactory.UserBId))?.Sale.Value;

            // Act
            await handler.Handle(
                new UpdateSaleCommand
                {
                    UserId = EShopDbContextFactory.UserBId,
                },
                CancellationToken.None);

            // Assert
            var newSaleValue = (await Context.Users.FirstOrDefaultAsync(user =>
                user.Id == EShopDbContextFactory.UserBId))?.Sale.Value;

            newSaleValue.ShouldNotBe(oldSaleValue);
            newSaleValue.ShouldBe(2.4);
        }

        [Fact]
        public async Task UpdateSaleCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateSaleCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateSaleCommand
                    {
                        UserId = Guid.NewGuid(),
                    },
                    CancellationToken.None));
        }
    }
}
