using EShop.Application.Features.Products.Models;
using EShop.Application.Features.Products.Queries.GetProductList;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.WebApi.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ProductListVm>> GetProducts()
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);
            var query = new GetProductListQuery { UserId = userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
