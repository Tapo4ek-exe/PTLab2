using EShop.Application.Features.Users.Commands.RegisterUser;
using EShop.Application.Services;
using EShop.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace EShop.Tests.Users.Commands
{
    public class RegisterCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task RegisterCommandHandler_Success()
        {
            // Arrange
            var handler = new RegisterCommandHandler(Context);
            var userEmail = "test@email.com";
            var userName = "Имя пользователя";
            var userPassword = "12345";

            // Act
            var jwt = await handler.Handle(
                new RegisterCommand
                {
                    Name = userName,
                    Email = userEmail,
                    Password = userPassword,
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Users.SingleOrDefaultAsync(user =>
                user.Name == userName &&
                user.Email == userEmail &&
                user.Password == HashService.HashPassword(userPassword)));
        }
    }
}
