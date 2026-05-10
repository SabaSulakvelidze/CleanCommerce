
using CleanCommerce.Domain.Enums;

namespace CleanCommerce.Application.DTOs.Responses
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status{ get; set; }
        public List<OrderItemResponse> Items { get; set; } = [];
    }
}
