using MediatR;

namespace EShop.Application.Features.Purchases.Commands.CreatePurchase
{
    public class CreatePurchaseCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Address { get; set; }
    }
}
