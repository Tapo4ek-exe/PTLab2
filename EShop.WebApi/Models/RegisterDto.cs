using AutoMapper;
using EShop.Application.Common.Mappings;
using EShop.Application.Features.Users.Commands.RegisterUser;

namespace EShop.WebApi.Models
{
    public class RegisterDto : IMapWith<RegisterCommand>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterDto, RegisterCommand>();
        }
    }
}
