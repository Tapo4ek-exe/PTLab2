using MediatR;

namespace EShop.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
