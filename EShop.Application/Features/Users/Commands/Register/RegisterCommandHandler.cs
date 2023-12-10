using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Domain;
using MediatR;

namespace EShop.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IEShopDbContext _dbContext;
        private readonly JwtService _jwtService;

        public RegisterCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
            _jwtService = new JwtService();
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Password = HashService.HashPassword(request.Password),
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _jwtService.GetJwtTokenForUser(user.Id);
        }
    }
}
