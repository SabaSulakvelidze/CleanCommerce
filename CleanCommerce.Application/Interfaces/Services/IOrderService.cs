
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;

namespace CleanCommerce.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request);
        Task<List<OrderListItemResponse>> GetMyOrderAsync();
        Task<OrderResponse?> GetMyOrderByIdAsync(int orderId);
        Task<List<OrderListItemResponse>> GetAllOrderAsync();
        Task<OrderResponse?> UpdateOrderStatusAsync(int orderId,UpdateOrderStatusRequest request);
    }
}
