using AutoMapper;
using EShop.Application.Features.Purchases.Commands.CreatePurchase;
using EShop.Application.Features.Purchases.Models;
using EShop.Application.Features.Purchases.Queries.GetPurchaseList;
using EShop.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.WebApi.Controllers
{
    public class PurchaseController : BaseController
    {
        private readonly IMapper _mapper;

        public PurchaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> CreatePurchase([FromBody] CreatePurchaseDto dto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);
            if (userIdClaim == null)
            {
                return BadRequest();
            }

            var command = _mapper.Map<CreatePurchaseCommand>(dto);
            command.UserId = Guid.Parse(userIdClaim.Value);
            var purchaseId = await Mediator.Send(command);
            return Ok(purchaseId);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PurchaseListVm>> GetUserPurchases()
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);
            if (userIdClaim == null)
            {
                return BadRequest();
            }

            var query = new GetPurchaseListQuery { UserId = Guid.Parse(userIdClaim.Value) };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
