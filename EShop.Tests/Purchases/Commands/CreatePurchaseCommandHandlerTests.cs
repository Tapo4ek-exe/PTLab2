using EShop.Application.Common.Exceptions;
using EShop.Application.Features.Purchases.Commands.CreatePurchase;
using EShop.Tests.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace EShop.Tests.Purchases.Commands
{
    public class CreatePurchaseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreatePurchaseCommandHandler_Success()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var handler = new CreatePurchaseCommandHandler(Context, mediator.Object);
            var address = "тестовый адрес";

            // Act
            var purchaseId = await handler.Handle(
                new CreatePurchaseCommand
                {
                    UserId = EShopDbContextFactory.UserAId,
                    ProductId = EShopDbContextFactory.Product2Id,
                    Address = address,
                },
                CancellationToken.None);

            // Assert
            purchaseId.ShouldNotBe(Guid.Empty);
            Assert.NotNull(await Context.Purchases.SingleOrDefaultAsync(purchase =>
                    purchase.Id == purchaseId &&
                    purchase.UserId == EShopDbContextFactory.UserAId &&
                    purchase.ProductId == EShopDbContextFactory.Product2Id &&
                    purchase.Address == address));
        }

        [Fact]
        public async Task CreatePurchaseCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var handler = new CreatePurchaseCommandHandler(Context, mediator.Object);
            var address = "тестовый адрес";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new CreatePurchaseCommand
                {
                    UserId = Guid.NewGuid(),
                    ProductId = EShopDbContextFactory.Product2Id,
                    Address = address,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task CreatePurchaseCommandHandler_FailOnWrongProductId()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var handler = new CreatePurchaseCommandHandler(Context, mediator.Object);
            var address = "тестовый адрес";

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new CreatePurchaseCommand
                {
                    UserId = EShopDbContextFactory.UserAId,
                    ProductId = Guid.NewGuid(),
                    Address = address,
                },
                CancellationToken.None));
        }
    }
}
