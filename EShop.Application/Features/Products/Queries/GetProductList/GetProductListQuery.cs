using EShop.Application.Features.Products.Models;
using MediatR;

namespace EShop.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<ProductListVm>
    {
        public Guid? UserId { get; set; }
    }
}
