
using CleanCommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CleanCommerce.Application.DTOs.Requests
{
    public class UpdateOrderStatusRequest
    {
        [Required]
        public OrderStatus Status{ get; set; }
    }
}
