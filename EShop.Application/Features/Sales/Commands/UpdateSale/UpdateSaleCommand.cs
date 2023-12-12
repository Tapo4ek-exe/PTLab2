using MediatR;

namespace EShop.Application.Features.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
