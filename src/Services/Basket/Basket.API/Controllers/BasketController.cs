using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;
        public BasketController(IBasketRepository repository,DiscountGrpcService discountGrpcService)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{UserName}",Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string UserName)
        {
            var basket = await _repository.GetBasket(UserName);
			return Ok(basket ?? new ShoppingCart(UserName));
        }

        [HttpPost(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody]ShoppingCart shoppingCart)
        {
            foreach (var item in shoppingCart.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

			return Ok(await _repository.UpdateBasket(shoppingCart));
        }

        [HttpDelete(Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string UserName)
        {
			await _repository.DeleteBasket(UserName);
            return Ok();
        }

    }
}
