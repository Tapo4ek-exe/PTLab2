using AutoMapper;
using EShop.Application.Common.Mappings;
using EShop.Application.Features.Products.Models;
using EShop.Domain;

namespace EShop.Application.Features.Purchases.Models
{
    public class PurchaseDto : IMapWith<Purchase>
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public ProductDto Product { get; set; }
        public double UsedPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Purchase, PurchaseDto>()
                .ForMember(purchaseDto => purchaseDto.Date,
                opt => opt.MapFrom(purchase => purchase.Date.ToString("O")));
        }
    }
}
