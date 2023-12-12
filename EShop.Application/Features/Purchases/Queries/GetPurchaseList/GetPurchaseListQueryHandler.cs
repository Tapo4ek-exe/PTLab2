using AutoMapper;
using AutoMapper.QueryableExtensions;
using EShop.Application.Features.Purchases.Models;
using EShop.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Features.Purchases.Queries.GetPurchaseList
{
    public class GetPurchaseListQueryHandler : IRequestHandler<GetPurchaseListQuery, PurchaseListVm>
    {
        private readonly IEShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPurchaseListQueryHandler(IEShopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PurchaseListVm> Handle(GetPurchaseListQuery request, CancellationToken cancellationToken)
        {
            var purchases = await _dbContext.Purchases.Where(purchase => purchase.UserId == request.UserId)
                .OrderByDescending(purchase => purchase.Date)
                .ProjectTo<PurchaseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new PurchaseListVm { Purchases = purchases };
            return vm;
        }
    }
}
