using AutoMapper;
using EShop.Application.Common.Mappings;
using EShop.Application.Features.Users.Queries.Login;

namespace EShop.WebApi.Models
{
    public class LoginDto : IMapWith<LoginQuery>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, LoginQuery>();
        }
    }
}
