using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanCommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            return Ok(await orderService.CreateOrderAsync(request));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyOrders()
        {
            return Ok(await orderService.GetMyOrderAsync());
        }

        [HttpGet("me/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetMyOrderById(int orderId)
        {
            return Ok(await orderService.GetMyOrderByIdAsync(orderId));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await orderService.GetAllOrderAsync());
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, UpdateOrderStatusRequest request)
        {
            return Ok(await orderService.UpdateOrderStatusAsync(orderId,request));
        }

    }
}
