using AutoMapper;
using EShop.Application.Features.Users.Commands.RegisterUser;
using EShop.Application.Features.Users.Queries.GetName;
using EShop.Application.Features.Users.Queries.Login;
using EShop.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> GetUserName()
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);
            if (userIdClaim == null)
            {
                return BadRequest();
            }
            var query = new GetUserNameQuery { UserId = Guid.Parse(userIdClaim.Value) };
            var userName = await Mediator.Send(query);
            return Ok(userName);
        }
    }
}
