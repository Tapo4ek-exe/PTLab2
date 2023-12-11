using EShop.Application.Features.Products.Models;
using EShop.Application.Features.Products.Queries.GetProductList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.WebApi.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductListVm>> GetProducts()
        {
            var query = new GetProductListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
