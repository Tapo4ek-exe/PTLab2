using EShop.Application.Common.Exceptions;
using EShop.Application.Interfaces;
using EShop.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IEShopDbContext _dbContext;
        private readonly JwtService _jwtService;

        public LoginQueryHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
            _jwtService = new JwtService();
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var passwordHash = HashService.HashPassword(request.Password);

            var user = await _dbContext.Users.FirstOrDefaultAsync(user =>
                user.Email == request.Email && user.Password == passwordHash);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), $"{request.Email};{request.Password}");
            }

            return _jwtService.GetJwtTokenForUser(user.Id);
        }
    }
}
