using EShop.Application.Common.Exceptions;
using EShop.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Users.Queries.GetName
{
    public class GetUserNameQueryHandler : IRequestHandler<GetUserNameQuery, string>
    {
        private readonly IEShopDbContext _dbContext;

        public GetUserNameQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserId);
            }
            return user.Name;
        }
    }
}
