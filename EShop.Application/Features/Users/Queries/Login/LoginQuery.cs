using MediatR;

namespace EShop.Application.Features.Users.Queries.Login
{
    public class LoginQuery : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
