using MediatR;

namespace EShop.Application.Features.Users.Queries.GetName
{
    public class GetUserNameQuery : IRequest<string>
    {
        public Guid UserId { get; set; }
    }
}
