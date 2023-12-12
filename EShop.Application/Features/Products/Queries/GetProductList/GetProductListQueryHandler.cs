using AutoMapper;
using AutoMapper.QueryableExtensions;
using EShop.Application.Features.Products.Models;
using EShop.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListVm>
    {
        private readonly IEShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IEShopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductListVm> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            foreach (var product in products)
            {
                product.SalePrice = product.Price;
            }

            if (request.UserId != null)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
                if (user != null)
                {
                    var sale = await _dbContext.Sales.FirstOrDefaultAsync(sale => sale.Id == user.SaleId, cancellationToken);
                    if (sale != null)
                    {
                        foreach (var product in products)
                        {
                            product.SalePrice = product.Price / 100 * (100 - sale.Value);
                        }
                    }
                }
            }

            var vm = new ProductListVm
            {
                Products = products,
            };
            return vm;
        }
    }
}
