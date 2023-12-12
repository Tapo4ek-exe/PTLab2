using EShop.Application.Features.Purchases.Models;
using MediatR;

namespace EShop.Application.Features.Purchases.Queries.GetPurchaseList
{
    public class GetPurchaseListQuery : IRequest<PurchaseListVm>
    {
        public Guid UserId { get; set; }
    }
}
