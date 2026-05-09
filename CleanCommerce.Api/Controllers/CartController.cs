using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyCart()
        {
            return Ok(await cartService.GetMyCartAsync());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            return Ok(await cartService.AddToCartAsync(request));
        }

        [HttpPut("{cartItemId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, UpdateCartItemRequest request)
        {
            return Ok(await cartService.UpdateCartItemASync(cartItemId, request));
        }

        [HttpDelete("{cartItemId}")]
        [Authorize]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            return Ok(await cartService.RemoveCartItemAsync(cartItemId));
        }

        [HttpDelete("clear")]
        [Authorize]
        public async Task<IActionResult> ClearMyCart()
        {
            return Ok(await cartService.ClearMyCartAsync());
        }
    }
}
