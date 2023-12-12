using AutoMapper;
using EShop.Application.Common.Mappings;
using EShop.Application.Features.Purchases.Commands.CreatePurchase;

namespace EShop.WebApi.Models
{
    public class CreatePurchaseDto : IMapWith<CreatePurchaseCommand>
    {
        public Guid ProductId { get; set; }
        public string Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePurchaseDto, CreatePurchaseCommand>();
        }
    }
}
