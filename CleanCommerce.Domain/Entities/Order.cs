using CleanCommerce.Domain.Enums;
using System.Numerics;

namespace CleanCommerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        public User User { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
