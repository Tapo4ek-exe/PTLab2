using AutoMapper;
using EShop.Application.Features.Users.Commands.RegisterUser;
using EShop.Application.Features.Users.Queries.Login;
using EShop.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDto registerDto)
        {
            var command = _mapper.Map<RegisterCommand>(registerDto);
            var jwt = await Mediator.Send(command);
            return Ok(jwt);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            var query = _mapper.Map<LoginQuery>(loginDto);
            var jwt = await Mediator.Send(query);
            return Ok(jwt);
        }
    }
}
