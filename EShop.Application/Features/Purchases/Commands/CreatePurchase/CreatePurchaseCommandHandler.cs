using EShop.Application.Common.Exceptions;
using EShop.Application.Features.Sales.Commands.UpdateSale;
using EShop.Application.Interfaces;
using EShop.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Features.Purchases.Commands.CreatePurchase
{
    public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, Guid>
    {
        private readonly IEShopDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreatePurchaseCommandHandler(IEShopDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            

            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserId);
            }

            var product = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException(nameof(product), request.ProductId);
            }

            var sale = await _dbContext.Sales.FirstOrDefaultAsync(sale => sale.Id == user.SaleId, cancellationToken);
            if (sale == null)
            {
                throw new NotFoundException(nameof(sale), user.SaleId);
            }

            var purchase = new Purchase
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Address = request.Address,
                ProductId = product.Id,
                UserId = user.Id,
                UsedPrice = product.Price / 100 * (100 - sale.Value),
            };

            await _dbContext.Purchases.AddAsync(purchase, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var updateSaleCommand = new UpdateSaleCommand { UserId = request.UserId };
            await _mediator.Send(updateSaleCommand);

            return purchase.Id;
        }
    }
}
