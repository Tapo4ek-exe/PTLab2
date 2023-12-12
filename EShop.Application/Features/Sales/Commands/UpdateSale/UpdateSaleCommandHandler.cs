using EShop.Application.Common.Exceptions;
using EShop.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand>
    {
        private readonly IEShopDbContext _dbContext;

        public UpdateSaleCommandHandler(IEShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserId);
            }

            var sale = await _dbContext.Sales.FirstOrDefaultAsync(sale => sale.Id == user.SaleId, cancellationToken);
            if (sale == null)
            {
                throw new NotFoundException(nameof(sale), user.SaleId);
            }

            var totalExpenses = await _dbContext.Purchases.Where(purchase => purchase.UserId == user.Id)
                    .SumAsync(p => p.UsedPrice, cancellationToken);
            
            sale.Value = Math.Min(totalExpenses / 10000, 25);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
