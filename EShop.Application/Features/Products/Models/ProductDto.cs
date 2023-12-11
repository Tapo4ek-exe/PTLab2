using AutoMapper;
using EShop.Application.Common.Mappings;
using EShop.Domain;

namespace EShop.Application.Features.Products.Models
{
    public class ProductDto : IMapWith<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(productDto => productDto.Id,
                opt => opt.MapFrom(product => product.Id.ToString()));
        }
    }
}
